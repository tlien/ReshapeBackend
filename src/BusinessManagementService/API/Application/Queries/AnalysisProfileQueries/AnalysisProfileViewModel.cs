using System;

namespace Reshape.BusinessManagementService.API.Application.Queries.AnalysisProfileQueries
{
    public class AnalysisProfileViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public MediaTypeViewModel MediaType { get; set; }
        public ScriptFileViewModel ScriptFile { get; set; }
        public ScriptParametersFileViewModel ScriptParametersFile { get; set; }
    }

    public class MediaTypeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class ScriptFileViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Script { get; set; }
    }

    public class ScriptParametersFileViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ScriptParameters { get; set; }
    }
}