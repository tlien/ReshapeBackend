using System;

using Reshape.Common.SeedWork;

namespace Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate
{
    public class AnalysisProfile : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public Guid MediaTypeId { get; private set; }
        public Guid ScriptFileId { get; private set; }
        public Guid ScriptParametersFileId { get; private set; }
        public virtual MediaType MediaType { get; private set; }
        public virtual ScriptFile ScriptFile { get; private set; }
        public virtual ScriptParametersFile ScriptParametersFile { get; private set; }

        public AnalysisProfile(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public void SetScriptFile(ScriptFile scriptFile)
        {
            ScriptFile = scriptFile;
        }

        public void SetScriptParametersFile(ScriptParametersFile scriptParametersFile)
        {
            ScriptParametersFile = scriptParametersFile;
        }

        public void SetMediaType(MediaType mediaType)
        {
            MediaType = mediaType;
        }
    }
}