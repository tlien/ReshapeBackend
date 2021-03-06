using System;
using Newtonsoft.Json;

using Reshape.Common.SeedWork;

namespace Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate
{
    /// <summary>
    /// Domain entity that will be used to provide parameters
    /// (arguments) for <c>AnalysisProfile.ScriptFile</c>.
    /// </summary>
    public class ScriptParametersFile : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ScriptParameters { get; private set; }

        [JsonConstructor]
        public ScriptParametersFile(Guid id, string name, string description, string scriptParameters)
        {
            base.Id = id;
            Name = name;
            Description = description;
            ScriptParameters = scriptParameters;
        }

        public ScriptParametersFile(string name, string description, string scriptParameters)
        {
            Name = name;
            Description = description;
            ScriptParameters = scriptParameters;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetScriptParameters(string scriptParameters)
        {
            ScriptParameters = scriptParameters;
        }
    }
}