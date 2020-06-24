using System;

namespace Reshape.AccountService.API.Application.Queries.AccountAdditionsQueries
{
    /// <summary>
    /// Represents a <c>Feature</c> from the account domains point of view.
    /// </summary>
    public class FeatureViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    /// <summary>
    /// Represents a <c>BusinessTier</c> from the account domains point of view.
    /// </summary>
    public class BusinessTierViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    /// <summary>
    /// Represents an <c>AnalysisProfile</c> from the account domains point of view.
    /// </summary>
    public class AnalysisProfileViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}