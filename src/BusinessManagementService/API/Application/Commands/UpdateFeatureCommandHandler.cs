using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

using Reshape.BusinessManagementService.API.Application.IntegrationEvents.Events;
using Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;
using Reshape.Common.EventBus.Services;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    public class UpdateFeatureCommandHandler : IRequestHandler<UpdateFeatureCommand, FeatureDTO>
    {
        private readonly IFeatureRepository _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IIntegrationEventService _integrationEventService;

        public UpdateFeatureCommandHandler(IFeatureRepository repository, IMediator mediator, IMapper mapper, IIntegrationEventService integrationEventService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _integrationEventService = integrationEventService ?? throw new ArgumentNullException(nameof(integrationEventService));
        }

        public async Task<FeatureDTO> Handle(UpdateFeatureCommand request, CancellationToken cancellationToken)
        {
            var feature = await _repository.GetAsync(request.Id);

            feature.SetName(request.Name);
            feature.SetDescription(request.Description);
            feature.SetPrice(request.Price);

            _repository.Update(feature);

            await _repository.UnitOfWork.SaveChangesAsync();

            var featureDTO = _mapper.Map<FeatureDTO>(feature);
            var integrationEvent = new FeatureUpdatedEvent(featureDTO);
            await _integrationEventService.AddAndSaveEventAsync(integrationEvent);

            return featureDTO;
        }
    }
}