using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.AccountService.API.Application.Commands
{
    [DataContract]
    public class AddFeaturesCommand : IRequest<int>
    {
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid AccountId { get; private set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public List<Guid> FeatureIds { get; private set; }

        public AddFeaturesCommand(Guid accountId, List<Guid> featureIds)
        {
            AccountId = accountId;
            FeatureIds = featureIds;
        }
    }
}