using System;
using System.Runtime.Serialization;
using MediatR;

namespace Reshape.AccountService.API.Application.Commands
{
    [DataContract]
    public class SetBusinessTierCommand : IRequest<int>
    {
        [DataMember]
        public Guid AccountId { get; private set; }

        [DataMember]
        public Guid BusinessTierId { get; private set; }

        public SetBusinessTierCommand(Guid accountId, Guid businessTierId)
        {
            AccountId = accountId;
            BusinessTierId = businessTierId;
        }
    }
}