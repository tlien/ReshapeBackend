
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Application.Commands
{
    public class AddFeaturesCommandHandler : IRequestHandler<AddFeaturesCommand, int>
    {
        private readonly IAccountRepository _accountRepository;

        public AddFeaturesCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public async Task<int> Handle(AddFeaturesCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAsync(request.AccountId);
            var features = await _accountRepository.GetFeaturesAsync(request.FeatureIds);

            features.ForEach(f => account.AddFeature(f));

            _accountRepository.Update(account);

            return await _accountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
