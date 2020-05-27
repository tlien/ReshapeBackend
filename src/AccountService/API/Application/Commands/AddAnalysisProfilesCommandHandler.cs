
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Commands
{
    public class AddAnalysisProfilesCommandHandler : IRequestHandler<AddAnalysisProfilesCommand, int>
    {
        private readonly IMediator _mediator;
        private readonly IAccountRepository _accountRepository;

        public AddAnalysisProfilesCommandHandler(IMediator mediator, IAccountRepository accountRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public async Task<int> Handle(AddAnalysisProfilesCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAsync(request.AccountId);
            var analysisProfiles = await _accountRepository.GetAnalysisProfilesAsync(request.AnalysisProfileIds);

            analysisProfiles.ForEach(ap => account.AddAnalysisProfile(ap));

            _accountRepository.Update(account);

            return await _accountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}