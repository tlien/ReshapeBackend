
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Application.Commands
{
    public class RemoveAnalysisProfilesCommandHandler : IRequestHandler<RemoveAnalysisProfilesCommand, int>
    {
        private readonly IAccountRepository _accountRepository;

        public RemoveAnalysisProfilesCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public async Task<int> Handle(RemoveAnalysisProfilesCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAsync(request.AccountId);
            var analysisProfiles = await _accountRepository.GetAnalysisProfilesAsync(request.AnalysisProfileIds);

            analysisProfiles.ForEach(ap => account.RemoveAnalysisProfile(ap));

            _accountRepository.Update(account);

            return await _accountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}