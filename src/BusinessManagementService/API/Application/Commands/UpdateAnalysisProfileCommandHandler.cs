using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

using Reshape.BusinessManagementService.API.Application.IntegrationEvents.Events;
using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Reshape.Common.EventBus.Services;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    public class UpdateAnalysisProfileCommandHandler : IRequestHandler<UpdateAnalysisProfileCommand, AnalysisProfileDTO>
    {
        private readonly IAnalysisProfileRepository _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IIntegrationEventService _integrationEventService;

        public UpdateAnalysisProfileCommandHandler(IAnalysisProfileRepository repository, IMediator mediator, IMapper mapper, IIntegrationEventService integrationEventService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _integrationEventService = integrationEventService ?? throw new ArgumentNullException(nameof(integrationEventService));
        }

        public async Task<AnalysisProfileDTO> Handle(UpdateAnalysisProfileCommand request, CancellationToken cancellationToken)
        {
            var analysisProfile = await _repository.GetAsync(request.Id);

            analysisProfile.SetName(request.Name);
            analysisProfile.SetDescription(request.Description);
            analysisProfile.SetPrice(request.Price);

            _repository.Update(analysisProfile);

            await _repository.UnitOfWork.SaveChangesAsync();

            var analysisProfileDTO = _mapper.Map<AnalysisProfileDTO>(analysisProfile);
            var integrationEvent = new AnalysisProfileUpdatedEvent(analysisProfileDTO);
            await _integrationEventService.AddAndSaveEventAsync(integrationEvent);

            return analysisProfileDTO;
        }
    }
}