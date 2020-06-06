using System;
using System.Runtime.Serialization;
using MediatR;

namespace Reshape.AccountService.API.Application.Commands
{
    [DataContract]
    public class SetContactDetailsCommand : IRequest<int>
    {
        [DataMember]
        public Guid AccountId { get; private set; }

        [DataMember]
        public string ContactPersonFullName { get; private set; }
        [DataMember]
        public string Phone { get; private set; }
        [DataMember]
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