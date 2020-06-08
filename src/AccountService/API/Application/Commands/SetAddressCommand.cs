using System;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.AccountService.API.Application.Commands
{
    [DataContract]
    public class SetAddressCommand : IRequest<int>
    {
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid AccountId { get; private set; }

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

        public SetAddressCommand(Guid accountId, string street1, string street2, string city, string zipCode, string country)
        {
            AccountId = accountId;
            Street1 = street1;
            Street2 = street2;
            City = city;
            ZipCode = zipCode;
            Country = country;
        }
    }
}