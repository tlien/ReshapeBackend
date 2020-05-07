using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using MediatR;
using static BusinessManagementService.API.Application.Commands.AnalysisProfileCommands.CreateAnalysisProfileCommand;
using static BusinessManagementService.API.Application.Commands.AnalysisProfileCommands.CreateAnalysisProfileCommandHandler;

namespace BusinessManagementService.API.Application.Commands.AnalysisProfileCommands
{
    public class CreateAnalysisProfileCommandHandler : IRequestHandler<CreateAnalysisProfileCommand, AnalysisProfileDTO>
    {
        private readonly IAnalysisProfileRepository _repository;
        private readonly IMediator _mediator;

        public CreateAnalysisProfileCommandHandler(IAnalysisProfileRepository repository, IMediator mediator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<AnalysisProfileDTO> Handle(CreateAnalysisProfileCommand message, CancellationToken cancellationToken)
        {
            var analysisProfile = new AnalysisProfile(message.Name, message.Description, message.FileName);
            _repository.Add(analysisProfile);
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return AnalysisProfileDTO.FromAnalysisProfile(analysisProfile);
        }

        public class AnalysisProfileDTO
        {
            public IEnumerable<AnalysisProfileRequiredFeatureDTO> RequiredFeatures { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string FileName { get; set; }

            public static AnalysisProfileDTO FromAnalysisProfile(AnalysisProfile analysisProfile) 
            {
                return new AnalysisProfileDTO
                {

                    Name = analysisProfile.GetName,
                    Description = analysisProfile.GetDescription,
                    FileName = analysisProfile.GetFileName,
                    RequiredFeatures = analysisProfile.RequiredFeatures.Select(rf => new AnalysisProfileRequiredFeatureDTO
                    {
                        AnalysisProfileID = rf.AnalysisProfileID,
                        FeatureID = rf.FeatureID
                    }
                    )
                };
            }
        }

    }
}