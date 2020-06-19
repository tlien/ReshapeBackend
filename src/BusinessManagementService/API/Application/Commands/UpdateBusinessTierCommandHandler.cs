using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

using Reshape.BusinessManagementService.API.Application.IntegrationEvents.Events;
using Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;
using Reshape.Common.EventBus.Services;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    public class UpdateBusinessTierCommandHandler : IRequestHandler<UpdateBusinessTierCommand, BusinessTierDTO>
    {
        private readonly IBusinessTierRepository _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IIntegrationEventService _integrationEventService;

        public UpdateBusinessTierCommandHandler(IBusinessTierRepository repository, IMediator mediator, IMapper mapper, IIntegrationEventService integrationEventService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _integrationEventService = integrationEventService ?? throw new ArgumentNullException(nameof(integrationEventService));
        }

        public async Task<BusinessTierDTO> Handle(UpdateBusinessTierCommand request, CancellationToken cancellationToken)
        {
            var businessTier = await _repository.GetAsync(request.Id);

            businessTier.SetName(request.Name);
            businessTier.SetDescription(request.Description);
            businessTier.SetPrice(request.Price);

            _repository.Update(businessTier);

            await _repository.UnitOfWork.SaveChangesAsync();

            var businessTierDTO = _mapper.Map<BusinessTierDTO>(businessTier);
            var integrationEvent = new BusinessTierUpdatedEvent(businessTierDTO);
            await _integrationEventService.AddAndSaveEventAsync(integrationEvent);

            return businessTierDTO;
        }
    }
}