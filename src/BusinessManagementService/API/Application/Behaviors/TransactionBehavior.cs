using System.Threading;
using System.Threading.Tasks;
using Reshape.BusinessManagementService.Infrastructure;
using MediatR;

namespace Reshape.BusinessManagementService.API.Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly BusinessManagementContext _context;

        // public TransactionBehavior(BusinessManagementContext context, )
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            throw new System.NotImplementedException();
        }
    }
}