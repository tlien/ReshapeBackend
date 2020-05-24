using System;

namespace BusinessManagementService.API.Application.Queries.FeatureQueries
{
    public class FeatureViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}