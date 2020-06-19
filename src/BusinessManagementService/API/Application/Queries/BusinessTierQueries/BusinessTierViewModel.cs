using System;

namespace Reshape.BusinessManagementService.API.Application.Queries.BusinessTierQueries
{
    public class BusinessTierViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}