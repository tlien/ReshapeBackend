using System.Collections.Generic;
using Reshape.Common.SeedWork;

namespace Reshape.AccountService.Domain.AggregatesModel.AccountAggregate
{
    public class Address : ValueObject
    {
        public string Street1 { get; private set; }
        public string Street2 { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
        public string Country { get; private set; }

        public Address() { }

        public Address(string street1, string street2, string city, string zipCode, string country)
        {
            Street1 = street1;
            Street2 = street2;
            City = city;
            ZipCode = zipCode;
            Country = country;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street1;
            yield return Street2;
            yield return City;
            yield return ZipCode;
            yield return Country;
        }
    }
}