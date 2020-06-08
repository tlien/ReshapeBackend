using System;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.AccountService.API.Application.Commands
{
    [DataContract]
    public class DeactivateAccountCommand : IRequest<int>
    {
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid AccountId { get; private set; }

        public DeactivateAccountCommand(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}