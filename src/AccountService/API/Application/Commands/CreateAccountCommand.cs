using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;

namespace Reshape.AccountService.API.Commands
{
    [DataContract]
    public class CreateAccountCommand : IRequest<bool>
    {
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

        [DataMember]
        public string ContactPersonFullName { get; private set; }
        [DataMember]
        public string Phone { get; private set; }
        [DataMember]
        public string Email { get; private set; }

        [DataMember]
        public BusinessTierDTO BusinessTier { get; private set; }
        [DataMember]
        private readonly List<FeatureDTO> _features;
        [DataMember]
        public IEnumerable<FeatureDTO> Features => _features;

        public CreateAccountCommand()
        {
            _features = new List<FeatureDTO>();
        }

        public CreateAccountCommand(string street1, string street2, string city, string zipCode, string country, string contactPersonFullName, string phone, string email, BusinessTierDTO businessTier, List<FeatureDTO> features) : this()
        {
            Street1 = street1;
            Street2 = street2;
            City = city;
            ZipCode = zipCode;
            Country = country;
            ContactPersonFullName = contactPersonFullName;
            Phone = phone;
            Email = email;
            BusinessTier = businessTier;
            _features = features;
        }
    }

    public class FeatureDTO
    {
        public string name { get; set; }
        public string description { get; set; }
    }

    public class BusinessTierDTO
    {
        public string name { get; set; }
    }
}