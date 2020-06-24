
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Application.Commands
{
    public class RemoveFeaturesCommandHandler : IRequestHandler<RemoveFeaturesCommand, int>
    {
        private readonly IAccountRepository _accountRepository;

        public RemoveFeaturesCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public async Task<int> Handle(RemoveFeaturesCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAsync(request.AccountId);
            var features = await _accountRepository.GetFeaturesAsync(request.FeatureIds);

            features.ForEach(f => account.RemoveFeature(f));

            _accountRepository.Update(account);

            return await _accountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
