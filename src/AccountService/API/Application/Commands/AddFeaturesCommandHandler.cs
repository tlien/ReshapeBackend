
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Commands
{
    public class AddFeaturesCommandHandler : IRequestHandler<AddFeaturesCommand, int>
    {
        private readonly IMediator _mediator;
        private readonly IAccountRepository _accountRepository;

        public AddFeaturesCommandHandler(IMediator mediator, IAccountRepository accountRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public async Task<int> Handle(AddFeaturesCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAsync(request.AccountId);
            var features = await _accountRepository.GetFeaturesAsync(request.FeatureIds);

            features.ForEach(f => account.AddFeatures(f));

            _accountRepository.Update(account);

            return await _accountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
