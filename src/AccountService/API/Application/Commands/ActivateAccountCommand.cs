using System;
using System.Runtime.Serialization;
using MediatR;

namespace Reshape.AccountService.API.Application.Commands
{
    [DataContract]
    public class ActivateAccountCommand : IRequest<int>
    {

        [DataMember]
        public Guid AccountId { get; private set; }

        public ActivateAccountCommand() { }

        public ActivateAccountCommand(Guid accountId) : this()
        {
            AccountId = accountId;
        }
    }
}