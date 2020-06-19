
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Application.Commands
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
    {
        private readonly IMediator _mediator;
        private readonly IAccountRepository _accountRepository;

        public CreateAccountCommandHandler(IMediator mediator, IAccountRepository accountRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var address = new Address(request.Street1, request.Street2, request.City, request.ZipCode, request.Country);
            var contactDetails = new ContactDetails(request.ContactPersonFullName, request.Phone, request.Email);
            var account = new Account(address, contactDetails);

            var businessTier = await _accountRepository.GetBusinessTierAsync(request.BusinessTierId);

            // TODO: gracefully handle case where businessTier isn't found in Db for whatever reason
            account.SetBusinessTier(businessTier);

            _accountRepository.Add(account);

            return await _accountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}