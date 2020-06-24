using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    public class UpdateScriptFileCommandHandler : IRequestHandler<UpdateScriptFileCommand, ScriptFileDTO>
    {
        private readonly IAnalysisProfileRepository _repository;
        private readonly IMapper _mapper;

        public UpdateScriptFileCommandHandler(IAnalysisProfileRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ScriptFileDTO> Handle(UpdateScriptFileCommand request, CancellationToken cancellationToken)
        {
            var scriptFile = await _repository.GetScriptFileAsync(request.Id);

            scriptFile.SetName(request.Name);
            scriptFile.SetDescription(request.Description);
            scriptFile.SetScript(request.Script);

            _repository.UpdateScriptFile(scriptFile);

            await _repository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<ScriptFileDTO>(scriptFile);
        }
    }
}