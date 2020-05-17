using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using MediatR;
using static BusinessManagementService.API.Application.Commands.CreateAnalysisProfileCommand;
using static BusinessManagementService.API.Application.Commands.CreateAnalysisProfileCommandHandler;

namespace BusinessManagementService.API.Application.Commands
{
    public class CreateAnalysisProfileCommandHandler : IRequestHandler<CreateAnalysisProfileCommand, AnalysisProfileDTO>
    {
        private readonly IAnalysisProfileRepository _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateAnalysisProfileCommandHandler(IAnalysisProfileRepository repository, IMediator mediator, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<AnalysisProfileDTO> Handle(CreateAnalysisProfileCommand message, CancellationToken cancellationToken)
        {
            // var analysisProfile = new AnalysisProfile(message.Name, message.Description, message.FileName);

            // foreach(var rf in message.RequiredFeatures)
            // {
            //     analysisProfile.AddRequiredFeature(new AnalysisProfileRequiredFeature { FeatureID = rf.FeatureID });
            // }

            // _repository.Add(analysisProfile);
            await _repository.UnitOfWork.SaveEntitiesAsync();

            // return _mapper.Map<AnalysisProfileDTO>(analysisProfile);
            
            return new AnalysisProfileDTO();
        }

        public class AnalysisProfileDTO
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string FileName { get; set; }
            public IEnumerable<AnalysisProfileRequiredFeatureDTO> RequiredFeatures { get; set; }

            // public static AnalysisProfileDTO FromAnalysisProfile(AnalysisProfile analysisProfile) 
            // {
            //     return new AnalysisProfileDTO()
            //     {

            //         Name = analysisProfile.GetName,
            //         Description = analysisProfile.GetDescription,
            //         FileName = analysisProfile.GetFileName,
            //         RequiredFeatures = analysisProfile.RequiredFeatures.Select(rf => new AnalysisProfileRequiredFeatureDTO
            //             {
            //                 AnalysisProfileID = rf.AnalysisProfileID,
            //                 FeatureID = rf.FeatureID
            //             }
            //         )
            //     };
            // }
        }

    }
}