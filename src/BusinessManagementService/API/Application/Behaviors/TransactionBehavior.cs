using System.Threading;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Reshape.BusinessManagementService.Infrastructure;
using MediatR;
using Reshape.BusinessManagementService.API.Application.IntegrationEvents;

namespace Reshape.BusinessManagementService.API.Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly BusinessManagementContext _context;
        private readonly IBusinessManagementIntegrationEventService _integrationEventService;

        public TransactionBehavior(BusinessManagementContext context, IBusinessManagementIntegrationEventService integrationEventService)
        {
            _context = context;
            _integrationEventService = integrationEventService;
        }
        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

            var response = default(TResponse);

            try
            {
                if(_context.HasActiveTransaction)
                {
                    // Console.WriteLine("Has active transaction.");
                    // response = await next();
                    // Console.WriteLine("Response: {0}", response);
                    // return response;
                    return await next();
                }

                // Execution strategy includes retry on failure
                var strategy = _context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;

                    using (var transaction = await _context.BeginTransactionAsync()) 
                    {
                        Console.WriteLine("Before");
                        response = await next();
                        Console.WriteLine("Response: {0}", response);
                        Console.WriteLine("After");
                        await _context.CommitTransactionAsync(transaction);

                        transactionId = transaction.TransactionId;
                    }

                    Console.WriteLine("TransactionId: {0}", transactionId);
                    await _integrationEventService.PublishEventsThroughEventBusAsync(transactionId);
                });

                return response;
            } catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}