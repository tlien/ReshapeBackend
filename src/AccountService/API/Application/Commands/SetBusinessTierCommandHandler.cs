
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Application.Commands
{
    public class SetBusinessTierCommandHandler : IRequestHandler<SetBusinessTierCommand, int>
    {
        private readonly IAccountRepository _accountRepository;

        public SetBusinessTierCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public async Task<int> Handle(SetBusinessTierCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAsync(request.AccountId);
            var businessTier = await _accountRepository.GetBusinessTierAsync(request.BusinessTierId);

            // TODO: gracefully handle case where businessTier isn't found in Db for whatever reason
            account.SetBusinessTier(businessTier);

            _accountRepository.Update(account);

            return await _accountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}