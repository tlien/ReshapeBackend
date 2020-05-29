using System.Collections.Generic;

using Reshape.Common.SeedWork;

namespace Reshape.AccountService.Domain.AggregatesModel.AccountAggregate
{
    public class ContactDetails : ValueObject
    {
        public string ContactPersonFullName { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public ContactDetails() { }

        public ContactDetails(string contactPersonFullName, string phone, string email)
        {
            ContactPersonFullName = contactPersonFullName;
            Phone = phone;
            Email = email;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return ContactPersonFullName;
            yield return Phone;
            yield return Email;
        }
    }
}