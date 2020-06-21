using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

using Reshape.Common.EventBus.Services;
using Reshape.BusinessManagementService.API.Application.IntegrationEvents.Events;
using Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    public class CreateFeatureCommandHandler : IRequestHandler<CreateFeatureCommand, FeatureDTO>
    {
        private readonly IFeatureRepository _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IIntegrationEventService _integrationEventService;

        public CreateFeatureCommandHandler(IFeatureRepository repository, IMediator mediator, IMapper mapper, IIntegrationEventService integrationEventService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _integrationEventService = integrationEventService;
        }

        public async Task<FeatureDTO> Handle(CreateFeatureCommand message, CancellationToken cancellationToken)
        {
            var feature = new Feature(message.Name, message.Description, message.Price);
            _repository.Add(feature);

            await _repository.UnitOfWork.SaveChangesAsync();

            var featureDTO = _mapper.Map<FeatureDTO>(feature);
            var integrationEvent = new FeatureCreatedEvent(featureDTO);
            await _integrationEventService.AddAndSaveEventAsync(integrationEvent);

            return featureDTO;
        }
    }

    public class FeatureDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}