using System;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.AccountService.API.Application.Commands
{
    /// <summary>
    /// Models the data needed to set the <c>BusinessTier</c> of an existing <c>Account</c> through the <c>SetBusinessTierCommandHandler</c>
    /// </summary>
    [DataContract]
    public class SetBusinessTierCommand : IRequest<int>
    {
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid AccountId { get; private set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid BusinessTierId { get; private set; }

        public SetBusinessTierCommand(Guid accountId, Guid businessTierId)
        {
            AccountId = accountId;
            BusinessTierId = businessTierId;
        }
    }
}