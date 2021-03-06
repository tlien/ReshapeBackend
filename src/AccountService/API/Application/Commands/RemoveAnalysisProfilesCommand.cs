using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.AccountService.API.Application.Commands
{
    /// <summary>
    /// Models the data needed to remove one or more <c>AnalysisProfiles</c> from an existing <c>Account</c> through the <c>RemoveAnalysisProfilesCommandHandler</c>
    /// </summary>
    [DataContract]
    public class RemoveAnalysisProfilesCommand : IRequest<int>
    {
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid AccountId { get; private set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public List<Guid> AnalysisProfileIds { get; private set; }

        public RemoveAnalysisProfilesCommand(Guid accountId, List<Guid> analysisProfileIds)
        {
            AccountId = accountId;
            AnalysisProfileIds = analysisProfileIds;
        }
    }
}