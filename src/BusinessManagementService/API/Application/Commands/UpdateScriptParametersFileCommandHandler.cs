using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    public class UpdateScriptParametersFileCommandHandler : IRequestHandler<UpdateScriptParametersFileCommand, ScriptParametersFileDTO>
    {
        private readonly IAnalysisProfileRepository _repository;
        private readonly IMapper _mapper;

        public UpdateScriptParametersFileCommandHandler(IAnalysisProfileRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ScriptParametersFileDTO> Handle(UpdateScriptParametersFileCommand request, CancellationToken cancellationToken)
        {
            var scriptParametersFile = await _repository.GetScriptParametersFileAsync(request.Id);

            scriptParametersFile.SetName(request.Name);
            scriptParametersFile.SetDescription(request.Description);
            scriptParametersFile.SetScriptParameters(request.ScriptParameters);

            _repository.UpdateScriptParametersFile(scriptParametersFile);

            await _repository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<ScriptParametersFileDTO>(scriptParametersFile);
        }
    }
}