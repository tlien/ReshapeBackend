using System;
using System.Collections.Generic;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfilePackageAggregate;
using Common.SeedWork;

namespace BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate {
    public class AnalysisProfile : Entity, IAggregateRoot {
        private string _name;
        private string _description;
        private decimal _price;
        private Guid _mediaTypeId;
        private Guid _scriptFileId;
         public Guid GetScriptFileId => _scriptFileId;
        private Guid _scriptParametersFileId;
        public MediaType MediaType { get; private set; }
        public ScriptFile ScriptFile { get; private set; }
        public ScriptParametersFile ScriptParametersFile { get; private set; }

        public AnalysisProfile(string name, string description, decimal price, Guid mediaTypeId, Guid scriptFileId,  Guid scriptParametersFileId)
        {
            _name = name;
            _description = description;
            _price = price;
            _mediaTypeId = mediaTypeId;
            _scriptFileId = scriptFileId;
            _scriptParametersFileId = scriptParametersFileId;
        }

        public void SetScriptFileId(Guid id)
        {
            _scriptFileId = id;
        }

        public void SetScriptParametersFileId(Guid id)
        {
            _scriptParametersFileId = id;
        }

        public void SetMediaTypeId(Guid id)
        {
            _mediaTypeId = id;
        }

        public void SetPrice(decimal price)
        {
            _price = price;
        }

    }
}