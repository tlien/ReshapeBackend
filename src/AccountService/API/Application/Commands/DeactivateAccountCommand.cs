using System;
using System.Runtime.Serialization;
using MediatR;

namespace Reshape.AccountService.API.Commands
{
    [DataContract]
    public class DeactivateAccountCommand : IRequest<int>
    {

        [DataMember]
        public Guid AccountId { get; private set; }

        public DeactivateAccountCommand() { }

        public DeactivateAccountCommand(Guid accountId) : this()
        {
            AccountId = accountId;
        }
    }
}