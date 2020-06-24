using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.AccountService.API.Application.Commands
{
    /// <summary>
    /// Models the data needed to add one or more <c>AnalysisProfiles</c> to an existing <c>Account</c> through the <c>AddAnalysisProfilesCommandHandler</c>
    /// </summary>
    [DataContract]
    public class AddAnalysisProfilesCommand : IRequest<int>
    {
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid AccountId { get; private set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public List<Guid> AnalysisProfileIds { get; private set; }

        public AddAnalysisProfilesCommand(Guid accountId, List<Guid> analysisProfileIds)
        {
            AccountId = accountId;
            AnalysisProfileIds = analysisProfileIds;
        }
    }
}