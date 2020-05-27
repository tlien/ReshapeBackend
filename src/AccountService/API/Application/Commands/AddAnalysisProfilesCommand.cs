using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;

namespace Reshape.AccountService.API.Commands
{
    [DataContract]
    public class AddAnalysisProfilesCommand : IRequest<int>
    {
        [DataMember]
        public Guid AccountId { get; private set; }

        [DataMember]
        public List<Guid> AnalysisProfileIds { get; private set; }

        public AddAnalysisProfilesCommand() { }

        public AddAnalysisProfilesCommand(Guid accountId, List<Guid> analysisProfileIds) : this()
        {
            AccountId = accountId;
            AnalysisProfileIds = analysisProfileIds;
        }
    }
}