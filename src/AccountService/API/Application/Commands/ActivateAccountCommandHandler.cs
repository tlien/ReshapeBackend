
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Commands
{
    public class ActivateAccountCommandHandler : IRequestHandler<ActivateAccountCommand, int>
    {
        private readonly IMediator _mediator;
        private readonly IAccountRepository _accountRepository;

        public ActivateAccountCommandHandler(IMediator mediator, IAccountRepository accountRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public async Task<int> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAsync(request.AccountId);

            account.SetAccountActive();

            _accountRepository.Update(account);

            return await _accountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}