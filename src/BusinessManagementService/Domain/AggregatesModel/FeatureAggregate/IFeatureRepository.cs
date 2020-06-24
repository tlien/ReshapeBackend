using System;
using System.Threading.Tasks;

using Reshape.Common.SeedWork;

namespace Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate
{
    /// <summary>
    /// Repository interface stating the methods necessary
    /// to handle database writes to the <c>Feature</c> domain aggregate.
    /// </summary>
    public interface IFeatureRepository : IRepository<Feature>
    {
        Feature Add(Feature feature);
        Task<Feature> GetAsync(Guid id);
        void Update(Feature feature);
    }
}