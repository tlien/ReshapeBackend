using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;
using MediatR;

namespace BusinessManagementService.API.Application.Commands
{
    public class CreateBusinessTierCommandHandler : IRequestHandler<CreateBusinessTierCommand, BusinessTierDTO>
    {
        private readonly IBusinessTierRepository _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateBusinessTierCommandHandler(IBusinessTierRepository repository, IMediator mediator, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BusinessTierDTO> Handle(CreateBusinessTierCommand message, CancellationToken cancellationToken)
        {
            var businessTier = new BusinessTier(message.Name, message.Description, message.Price);
            _repository.Add(businessTier);
            await _repository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<BusinessTierDTO>(businessTier);
        }
    }

    public class BusinessTierDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}