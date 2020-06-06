using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;
using MediatR;
using static Reshape.BusinessManagementService.API.Application.Commands.CreateFeatureCommandHandler;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    public class CreateFeatureCommandHandler : IRequestHandler<CreateFeatureCommand, FeatureDTO>
    {
        private readonly IFeatureRepository _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateFeatureCommandHandler(IFeatureRepository repository, IMediator mediator, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<FeatureDTO> Handle(CreateFeatureCommand message, CancellationToken cancellationToken)
        {
            var feature = new Feature(message.Name, message.Description, message.Price);
            _repository.Add(feature);
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<FeatureDTO>(feature);
        }

        public class FeatureDTO
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
        }
    }
}