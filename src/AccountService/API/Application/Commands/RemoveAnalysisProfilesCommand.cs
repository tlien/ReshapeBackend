using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;

namespace Reshape.AccountService.API.Application.Commands
{
    [DataContract]
    public class RemoveAnalysisProfilesCommand : IRequest<int>
    {
        [DataMember]
        public Guid AccountId { get; private set; }

        [DataMember]
        public List<Guid> AnalysisProfileIds { get; private set; }

        public RemoveAnalysisProfilesCommand(Guid accountId, List<Guid> analysisProfileIds)
        {
            AccountId = accountId;
            AnalysisProfileIds = analysisProfileIds;
        }
    }
}