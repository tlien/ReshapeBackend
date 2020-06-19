using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    public class SetScriptFileCommandHandler : IRequestHandler<SetScriptFileCommand, AnalysisProfileDTO>
    {
        private readonly IAnalysisProfileRepository _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SetScriptFileCommandHandler(IAnalysisProfileRepository repository, IMediator mediator, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<AnalysisProfileDTO> Handle(SetScriptFileCommand request, CancellationToken cancellationToken)
        {
            var analysisProfile = await _repository.GetAsync(request.AnalysisProfileId);
            var scriptFile = await _repository.GetScriptFileAsync(request.ScriptFileId);

            analysisProfile.SetScriptFile(scriptFile);
            _repository.Update(analysisProfile);

            await _repository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<AnalysisProfileDTO>(analysisProfile);
        }
    }
}