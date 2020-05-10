using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;
using MediatR;
using static BusinessManagementService.API.Application.Commands.CreateFeatureCommandHandler;

namespace BusinessManagementService.API.Application.Commands
{
    public class CreateFeatureCommandHandler : IRequestHandler<CreateFeatureCommand, FeatureDTO>
    {
        private readonly IFeatureRepository _repository;
        private readonly IMediator _mediator;

        public CreateFeatureCommandHandler(IFeatureRepository repository, IMediator mediator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<FeatureDTO> Handle(CreateFeatureCommand message, CancellationToken cancellationToken)
        {
            var feature = new Feature(message.Name, message.Description);
            _repository.Add(feature);
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return FeatureDTO.FromFeature(feature);
        }

        public class FeatureDTO
        {
            public string Name { get; set; }
            public string Description { get; set; }

            public static FeatureDTO FromFeature(Feature feature)
            {
                return new FeatureDTO()
                {
                    Name = feature.GetName,
                    Description = feature.GetDescription,
                };
            }
        }

    }
}