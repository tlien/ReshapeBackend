using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    public class UpdateMediaTypeCommandHandler : IRequestHandler<UpdateMediaTypeCommand, MediaTypeDTO>
    {
        private readonly IAnalysisProfileRepository _repository;
        private readonly IMapper _mapper;

        public UpdateMediaTypeCommandHandler(IAnalysisProfileRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<MediaTypeDTO> Handle(UpdateMediaTypeCommand request, CancellationToken cancellationToken)
        {
            var mediaType = await _repository.GetMediaTypeAsync(request.Id);

            mediaType.SetName(request.Name);

            _repository.UpdateMediaType(mediaType);

            await _repository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<MediaTypeDTO>(mediaType);
        }
    }
}