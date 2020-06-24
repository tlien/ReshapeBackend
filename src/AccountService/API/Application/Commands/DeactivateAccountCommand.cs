using System;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.AccountService.API.Application.Commands
{
    /// <summary>
    /// Models the data needed to deactive an existing <c>Account</c> through the <c>DeactivateAccountCommandHandler</c>
    /// </summary>
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