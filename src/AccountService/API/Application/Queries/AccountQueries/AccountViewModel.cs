using System;
using System.Collections.Generic;

namespace Reshape.AccountService.API.Application.Queries.AccountQueries
{
    /// <summary>
    /// Represents an <c>Account</c> aggregate.
    /// </summary>
    public class AccountViewModel
    {
        public Guid Id { get; set; }

        public bool IsActive { get; set; }

        public AddressViewModel Address { get; set; }
        public ContactDetailsViewModel ContactDetails { get; set; }

        public BusinessTierViewModel BusinessTier { get; set; }
        public List<FeatureViewModel> Features { get; set; }
        public List<AnalysisProfileViewModel> AnalysisProfiles { get; set; }
    }

    /// <summary>
    /// Represents an <c>Address</c> value object.
    /// </summary>
    public class AddressViewModel
    {
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }

    /// <summary>
    /// Represents a <c>ContactDetails</c> value object.
    /// </summary>
    public class ContactDetailsViewModel
    {
        public string ContactPersonFullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    /// <summary>
    /// Represents a <c>Feature</c> entity.
    /// </summary>
    public class FeatureViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// Represents a <c>BusinessTier</c> entity.
    /// </summary>
    public class BusinessTierViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// Represents a <c>AnalysisProfile</c> entity.
    /// </summary>
    public class AnalysisProfileViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}