
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Application.Commands
{
    public class SetContactDetailsCommandHandler : IRequestHandler<SetContactDetailsCommand, int>
    {
        private readonly IAccountRepository _accountRepository;

        public SetContactDetailsCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public async Task<int> Handle(SetContactDetailsCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAsync(request.AccountId);
            var contactDetails = new ContactDetails(request.ContactPersonFullName, request.Phone, request.Email);

            account.SetContactDetails(contactDetails);

            _accountRepository.Update(account);

            return await _accountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}