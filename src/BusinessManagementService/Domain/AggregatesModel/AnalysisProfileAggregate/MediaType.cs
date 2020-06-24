using System;
using Newtonsoft.Json;

using Reshape.Common.SeedWork;

namespace Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate
{
    /// <summary>
    /// The <c>MediaType</c> domain entity holds the name of a medium used
    /// by the <c>AnalysisProfile</c> domain aggregate.
    /// </summary>
    public class MediaType : Entity
    {
        public string Name { get; private set; }

        [JsonConstructor]
        public MediaType(Guid id, string name)
        {
            base.Id = id;
            Name = name;
        }

        public MediaType(string name)
        {
            Name = name;
        }

        public void SetName(string name)
        {
            Name = name;
        }
    }
}