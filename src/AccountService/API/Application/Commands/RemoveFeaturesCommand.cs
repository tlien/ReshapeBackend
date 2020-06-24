using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.AccountService.API.Application.Commands
{
    /// <summary>
    /// Models the data needed to remove one or more <c>Features</c> from an existing <c>Account</c> through the <c>RemoveFeaturesCommandHandler</c>
    /// </summary>
    [DataContract]
    public class RemoveFeaturesCommand : IRequest<int>
    {
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid AccountId { get; private set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public List<Guid> FeatureIds { get; private set; }

        public RemoveFeaturesCommand(Guid accountId, List<Guid> featureIds)
        {
            AccountId = accountId;
            FeatureIds = featureIds;
        }
    }
}