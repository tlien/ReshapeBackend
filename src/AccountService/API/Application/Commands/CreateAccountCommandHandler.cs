
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Commands
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

            foreach (var feature in request.Features)
            {
                account.AddFeatures(feature);
            }

            _accountRepository.Add(account);

            return await _accountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            throw new NotImplementedException();
        }
    }
}