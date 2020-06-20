
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Application.Commands
{
    public class AddAnalysisProfilesCommandHandler : IRequestHandler<AddAnalysisProfilesCommand, int>
    {
        private readonly IAccountRepository _accountRepository;

        public AddAnalysisProfilesCommandHandler(IAccountRepository accountRepository)
        {
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