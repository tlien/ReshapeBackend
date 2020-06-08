using System;
using Newtonsoft.Json;

using Reshape.Common.SeedWork;

namespace Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate
{
    public class ScriptFile : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Script { get; private set; }

        [JsonConstructor]
        public ScriptFile(Guid id, string name, string description, string script)
        {
            base.Id = id;
            Name = name;
            Description = description;
            Script = script;
        }

        public ScriptFile(string name, string description, string script)
        {
            Name = name;
            Description = description;
            Script = script;
        }
    }
}