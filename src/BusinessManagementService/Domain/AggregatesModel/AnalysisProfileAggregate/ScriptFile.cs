using Common.SeedWork;

namespace BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate
{
    public class ScriptFile : Entity 
    {
        private string _name;
        private string _description;
        private string _script;

        public ScriptFile(string name, string description, string script)
        {
            _name = name;
            _description = description;
            _script = script;
        }
    }
}