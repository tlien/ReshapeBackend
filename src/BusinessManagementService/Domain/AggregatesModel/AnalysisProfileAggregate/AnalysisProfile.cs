using System;
using System.Collections.Generic;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfilePackageAggregate;
using Common.SeedWork;

namespace BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate {
    public class AnalysisProfile : Entity, IAggregateRoot {
        private string _name;
        private string _description;
        private decimal _price;
        // private Guid _mediaTypeId;
        // private Guid _scriptFileId;
        // private Guid _scriptParametersFileId;

        public Guid MediaTypeId { get; private set; }
        public Guid ScriptFileId { get; private set; }
        public Guid ScriptParametersFileId { get; private set; }
        public MediaType MediaType { get; private set; }
        public ScriptFile ScriptFile { get; private set; }
        public ScriptParametersFile ScriptParametersFile { get; private set; }
        // private readonly List<AnalysisProfileAnalysisProfilePackage> _analysisProfileAnalysisProfilePackages;
        // public IReadOnlyCollection<AnalysisProfileAnalysisProfilePackage> AnalysisProfileAnalysisProfilePackages => _analysisProfileAnalysisProfilePackages;
        public string GetName => _name;
        public string GetDescription => _description;
        // public Guid GetMediaTypeId => _mediaTypeId;
        // public Guid GetScriptFileId => _scriptFileId;
        // public Guid GetScriptParametersFileId => _scriptParametersFileId;

        public AnalysisProfile()
        {
            // _analysisProfileAnalysisProfilePackages = new List<AnalysisProfileAnalysisProfilePackage>();
        }
        public AnalysisProfile(string name, string description, decimal price, Guid mediaTypeId, Guid scriptFileId,  Guid scriptParametersFileId) : this()
        {
            _name = name;
            _description = description;
            _price = price;
            // _mediaTypeId = mediaTypeId;
            // _scriptFileId = scriptFileId;
            // _scriptParametersFileId = scriptParametersFileId;
            MediaTypeId = mediaTypeId;
            ScriptFileId = scriptFileId;
            ScriptParametersFileId = scriptParametersFileId;
        }

        public void SetScriptFileId(Guid id)
        {
            // _scriptFileId = id;
            ScriptFileId = id;
        }

        public void SetScriptParametersFileId(Guid id)
        {
            // _scriptParametersFileId = id;
            ScriptParametersFileId = id;
        }

        public void SetMediaTypeId(Guid id)
        {
            // _mediaTypeId = id;
            MediaTypeId = id;
        }

        public void SetPrice(decimal price)
        {
            _price = price;
        }

    }
}