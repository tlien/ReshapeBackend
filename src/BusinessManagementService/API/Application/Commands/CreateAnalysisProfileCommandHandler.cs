using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

using Reshape.Common.EventBus.Services;
using Reshape.BusinessManagementService.API.Application.IntegrationEvents.Events;
using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    public class CreateAnalysisProfileCommandHandler : IRequestHandler<CreateAnalysisProfileCommand, AnalysisProfileDTO>
    {
        private readonly IAnalysisProfileRepository _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IIntegrationEventService _integrationEventService;

        public CreateAnalysisProfileCommandHandler(IAnalysisProfileRepository repository, IMediator mediator, IMapper mapper, IIntegrationEventService integrationEventService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _integrationEventService = integrationEventService;
        }

        public async Task<AnalysisProfileDTO> Handle(CreateAnalysisProfileCommand message, CancellationToken cancellationToken)
        {
            var analysisProfile = new AnalysisProfile(message.Name, message.Description, message.Price);
            analysisProfile.SetMediaType(_repository.AddMediaType(
                new MediaType(message.MediaType.Id, message.MediaType.Name))
            );
            analysisProfile.SetScriptFile(_repository.AddScriptFile(
                new ScriptFile(message.ScriptFile.Id, message.ScriptFile.Name, message.ScriptFile.Description, message.ScriptFile.Script)
                )
            );
            analysisProfile.SetScriptParametersFile(_repository.AddScriptParametersFile(
                new ScriptParametersFile(message.ScriptParametersFile.Id, message.ScriptParametersFile.Name, message.ScriptParametersFile.Description, message.ScriptParametersFile.ScriptParameters)
                )
            );

            _repository.Add(analysisProfile);
            await _repository.UnitOfWork.SaveChangesAsync();

            var analysisProfileDTO = _mapper.Map<AnalysisProfileDTO>(analysisProfile);

            var integrationEvent = new NewAnalysisProfileIntegrationEvent(analysisProfileDTO);
            await _integrationEventService.AddAndSaveEventAsync(integrationEvent);

            return analysisProfileDTO;
        }
    }

    public class AnalysisProfileDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public MediaTypeDTO MediaType { get; set; }
        public ScriptFileDTO ScriptFile { get; set; }
        public ScriptParametersFileDTO ScriptParametersFile { get; set; }
    }

    public class MediaTypeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class ScriptFileDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Script { get; set; }
    }

    public class ScriptParametersFileDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ScriptParameters { get; set; }
    }
}