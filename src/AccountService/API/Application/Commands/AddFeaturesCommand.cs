using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;

namespace Reshape.AccountService.API.Application.Commands
{
    [DataContract]
    public class AddFeaturesCommand : IRequest<int>
    {
        [DataMember]
        public Guid AccountId { get; private set; }

        [DataMember]
        public List<Guid> FeatureIds { get; private set; }

        public AddFeaturesCommand() { }

        public AddFeaturesCommand(Guid accountId, List<Guid> featureIds) : this()
        {
            AccountId = accountId;
            FeatureIds = featureIds;
        }
    }
}