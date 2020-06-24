
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Application.Commands
{
    public class SetAddressCommandHandler : IRequestHandler<SetAddressCommand, int>
    {
        private readonly IAccountRepository _accountRepository;

        public SetAddressCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public async Task<int> Handle(SetAddressCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAsync(request.AccountId);
            var address = new Address(request.Street1, request.Street2, request.City, request.ZipCode, request.Country);

            account.SetAddress(address);

            _accountRepository.Update(account);

            return await _accountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}