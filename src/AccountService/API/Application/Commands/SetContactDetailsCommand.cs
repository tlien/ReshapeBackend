using System;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.AccountService.API.Application.Commands
{
    [DataContract]
    public class SetContactDetailsCommand : IRequest<int>
    {
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid AccountId { get; private set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string ContactPersonFullName { get; private set; }
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string Phone { get; private set; }
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string Email { get; private set; }

        public SetContactDetailsCommand(Guid accountId, string contactPersonFullName, string phone, string email)
        {
            AccountId = accountId;
            ContactPersonFullName = contactPersonFullName;
            Phone = phone;
            Email = email;
        }
    }
}