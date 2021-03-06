using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MediatR;

using Reshape.Common.EventBus.Services;
using Reshape.BusinessManagementService.Infrastructure;

namespace Reshape.BusinessManagementService.API.Application.Behaviors
{
    /// <summary>
    /// <c>TransactionBehavior</c> extends <c>MediatR.IPipelineBehavior</c>.
    /// Through database transactions, it ensures that changes from incoming commands, as well as integration events they may spawn,
    /// are only committed if all changes go through. The pipeline is set in motion when a command is sent through the mediator.
    /// Numerous classes could extend the <c>IPipelineBehavior</c>, allowing additional functionality in different stages of the mediation of commands.
    /// To continue to the next step in the pipeline, the 'next' <c>RequestHandlerDelegate</c> is awaited.
    /// When all of the pipeline <c>RequestHandlerDelegates</c> have been awaited in sequence, the pipeline comes to a natural conclusion within the commandhandler's <c>Handle</c> method.
    /// </summary>
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;
        private readonly BusinessManagementContext _context;
        private readonly IIntegrationEventService _integrationEventService;

        public TransactionBehavior(BusinessManagementContext context, IIntegrationEventService integrationEventService, ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
            _context = context;
            _integrationEventService = integrationEventService;
        }

        /// <summary>
        /// Executes pipeline behavior:
        ///
        /// Create a new transaction if there is not already an active transaction within the database context,
        /// then await the pipeline result through the <c>RequestHandlerDelegate</c> 'next'.
        /// When the remainder of the pipeline has finished, publish whatever integration events that may have
        /// spawned during the transaction through the event bus.
        /// </summary>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);

            try
            {
                // Do not start a new transaction if an active one already exists
                if (_context.HasActiveTransaction)
                {
                    _logger.LogDebug("Has active transaction. Calling command handler.");
                    response = await next();
                    _logger.LogDebug("Response: {0}", response);
                    return response;
                }

                // Execution strategy includes retry on failure
                var strategy = _context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;

                    // Start a new transaction to ensure invariance in the domain
                    using (var transaction = await _context.BeginTransactionAsync())
                    {
                        _logger.LogDebug("New transaction begun. Calling command handler.");
                        response = await next();
                        _logger.LogDebug("Response: {0}", response);

                        // Commit transaction to database, if this fails a rollback of all changes recorded during the transaction will be rolled back
                        await _context.CommitTransactionAsync(transaction);

                        transactionId = transaction.TransactionId;
                    }

                    _logger.LogDebug("TransactionId: {0}", transactionId);
                    // Publish integration events to event bus if everything succeeded
                    await _integrationEventService.PublishEventsThroughEventBusAsync(transactionId);
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something terrible happened in the MediatR Pipeline!");
                throw;
            }
        }
    }
}