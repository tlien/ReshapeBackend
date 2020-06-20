using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

using Reshape.Common.EventBus.Services;
using Reshape.BusinessManagementService.API.Application.IntegrationEvents.Events;
using Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    public class CreateBusinessTierCommandHandler : IRequestHandler<CreateBusinessTierCommand, BusinessTierDTO>
    {
        private readonly IBusinessTierRepository _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IIntegrationEventService _integrationEventService;

        public CreateBusinessTierCommandHandler(IBusinessTierRepository repository, IMediator mediator, IMapper mapper, IIntegrationEventService integrationEventService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _integrationEventService = integrationEventService;
        }

        public async Task<BusinessTierDTO> Handle(CreateBusinessTierCommand message, CancellationToken cancellationToken)
        {
            var businessTier = new BusinessTier(message.Name, message.Description, message.Price);
            _repository.Add(businessTier);

            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            var businessTierDTO = _mapper.Map<BusinessTierDTO>(businessTier);
            var integrationEvent = new BusinessTierCreatedEvent(businessTierDTO);
            await _integrationEventService.AddAndSaveEventAsync(integrationEvent);

            return businessTierDTO;
        }
    }

    public class BusinessTierDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}