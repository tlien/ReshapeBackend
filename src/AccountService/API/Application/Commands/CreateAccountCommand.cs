using System;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.AccountService.API.Application.Commands
{
    [DataContract]
    public class CreateAccountCommand : IRequest<int>
    {
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string Street1 { get; private set; }
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string Street2 { get; private set; }
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string City { get; private set; }
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string ZipCode { get; private set; }
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string Country { get; private set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string ContactPersonFullName { get; private set; }
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string Phone { get; private set; }
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string Email { get; private set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid BusinessTierId { get; private set; }

        public CreateAccountCommand(string street1, string street2, string city, string zipCode, string country, string contactPersonFullName, string phone, string email, Guid businessTierId)
        {
            Street1 = street1;
            Street2 = street2;
            City = city;
            ZipCode = zipCode;
            Country = country;
            ContactPersonFullName = contactPersonFullName;
            Phone = phone;
            Email = email;
            BusinessTierId = businessTierId;
        }
    }
}