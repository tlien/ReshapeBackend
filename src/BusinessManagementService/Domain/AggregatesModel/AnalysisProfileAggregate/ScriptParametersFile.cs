using Common.SeedWork;

namespace BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate
{
    public class ScriptParametersFile : Entity
    {
        private string _name;
        private string _description;
        private string _scriptParameters;

        public ScriptParametersFile(string name, string description, string scriptParameters)
        {
            _name = name;
            _description = description;
            _scriptParameters = scriptParameters;
        }
    }
}