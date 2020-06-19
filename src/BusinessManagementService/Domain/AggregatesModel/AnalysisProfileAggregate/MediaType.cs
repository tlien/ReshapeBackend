using System;
using Newtonsoft.Json;

using Reshape.Common.SeedWork;

namespace Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate
{
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