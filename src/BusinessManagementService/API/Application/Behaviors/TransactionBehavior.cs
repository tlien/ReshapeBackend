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

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

            var response = default(TResponse);

            try
            {
                if (_context.HasActiveTransaction)
                {
                    _logger.LogDebug("Has active transaction.");
                    response = await next();
                    _logger.LogDebug("Response: {0}", response);
                    return response;
                    // return await next();
                }

                // Execution strategy includes retry on failure
                var strategy = _context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;

                    using (var transaction = await _context.BeginTransactionAsync())
                    {
                        _logger.LogDebug("Before");
                        response = await next();
                        _logger.LogDebug("Response: {0}", response);
                        _logger.LogDebug("After");
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