using Common.SeedWork;

namespace BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate
{
    public class MediaType : Entity {
        private string _name;
        public string GetName => _name;

        public MediaType(string name) {
            _name = name;
        }
    }
}