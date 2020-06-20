using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MediatR;

using Reshape.Common.EventBus.Services;
using Reshape.AccountService.Infrastructure;

namespace Reshape.AccountService.API.Application.Behaviors
{

    /// <summary>
    /// TransactionBehavior extends the MediatR IPipelineBehavior.
    /// Through transactions, it ensures that changes from incoming commands are only committed if all changes go through.
    /// The pipeline is set in motion when a command is sent through the mediator.
    /// Numerous classes could extend the IPipelineBehavior, allowing additional functionality in different stages of the mediation of commands.
    /// To continue to the next step in the pipeline, the 'next' RequestHandlerDelegate is awaited.
    /// When all of the pipeline RequestHandlerDelegates have been awaited in sequence, the pipeline comes to a natural conclusion within the commandhandler's Handle method.
    /// </summary>

    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;
        private readonly AccountContext _context;
        private readonly IIntegrationEventService _integrationEventService;

        public TransactionBehavior(AccountContext context, IIntegrationEventService integrationEventService, ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
            _context = context;
            _integrationEventService = integrationEventService;
        }

        /// <summary>
        /// Execute pipeline behavior.
        /// Proceed to the next step in the pipeline by awaiting the result of the 'next' RequestHandlerDelegate.
        /// </summary>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);

            try
            {
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

                    using (var transaction = await _context.BeginTransactionAsync())
                    {
                        _logger.LogDebug("New transaction begun. Calling command handler.");
                        response = await next();
                        _logger.LogDebug("Response: {0}", response);
                        await _context.CommitTransactionAsync(transaction);

                        transactionId = transaction.TransactionId;
                    }

                    _logger.LogDebug("TransactionId: {0}", transactionId);
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