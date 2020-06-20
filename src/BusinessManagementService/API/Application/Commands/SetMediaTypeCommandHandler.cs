using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    public class SetMediaTypeCommandHandler : IRequestHandler<SetMediaTypeCommand, AnalysisProfileDTO>
    {
        private readonly IAnalysisProfileRepository _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SetMediaTypeCommandHandler(IAnalysisProfileRepository repository, IMediator mediator, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<AnalysisProfileDTO> Handle(SetMediaTypeCommand request, CancellationToken cancellationToken)
        {
            var analysisProfile = await _repository.GetAsync(request.AnalysisProfileId);
            var mediaType = await _repository.GetMediaTypeAsync(request.MediaTypeId);

            analysisProfile.SetMediaType(mediaType);
            _repository.Update(analysisProfile);

            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AnalysisProfileDTO>(analysisProfile);
        }
    }
}