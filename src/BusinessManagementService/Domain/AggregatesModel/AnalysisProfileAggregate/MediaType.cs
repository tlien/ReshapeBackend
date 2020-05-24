using System;
using Common.SeedWork;

namespace BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate
{
    public class MediaType : Entity {
        public string Name { get; private set; }
        public MediaType(Guid id, string name) {
            base.Id = id;
            Name = name;
        }

        public MediaType(string name) {
            Name = name;
        }
    }
}