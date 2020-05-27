using System;
using System.Runtime.Serialization;
using MediatR;

namespace Reshape.AccountService.API.Commands
{
    [DataContract]
    public class ContactDetailsCommand : IRequest<int>
    {
        [DataMember]
        public Guid AccountId { get; private set; }

        [DataMember]
        public string ContactPersonFullName { get; private set; }
        [DataMember]
        public string Phone { get; private set; }
        [DataMember]
        public string Email { get; private set; }

        public ContactDetailsCommand() { }

        public ContactDetailsCommand(Guid accountId, string contactPersonFullName, string phone, string email) : this()
        {
            AccountId = accountId;
            ContactPersonFullName = contactPersonFullName;
            Phone = phone;
            Email = email;
        }
    }
}