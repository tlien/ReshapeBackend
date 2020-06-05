using System;
using System.Runtime.Serialization;
using MediatR;

namespace Reshape.AccountService.API.Application.Commands
{
    [DataContract]
    public class DeactivateAccountCommand : IRequest<int>
    {
        [DataMember]
        public Guid AccountId { get; private set; }

        public DeactivateAccountCommand(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}