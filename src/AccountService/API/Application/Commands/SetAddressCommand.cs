using System;
using System.Runtime.Serialization;
using MediatR;

namespace Reshape.AccountService.API.Application.Commands
{
    [DataContract]
    public class SetAddressCommand : IRequest<int>
    {
        [DataMember]
        public Guid AccountId { get; private set; }

        [DataMember]
        public string Street1 { get; private set; }
        [DataMember]
        public string Street2 { get; private set; }
        [DataMember]
        public string City { get; private set; }
        [DataMember]
        public string ZipCode { get; private set; }
        [DataMember]
        public string Country { get; private set; }

        public SetAddressCommand() { }

        public SetAddressCommand(Guid accountId, string street1, string street2, string city, string zipCode, string country) : this()
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