using System;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.AccountService.API.Application.Commands
{
    /// <summary>
    /// Models the data needed to active an existing <c>Account</c> through the <c>ActivateAccountCommandHandler</c>
    /// </summary>
    [DataContract]
    public class ActivateAccountCommand : IRequest<int>
    {
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid AccountId { get; private set; }

        public ActivateAccountCommand(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}