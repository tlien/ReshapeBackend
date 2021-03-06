using AutoMapper;

using Reshape.BusinessManagementService.API.Application.Commands;
using Reshape.BusinessManagementService.API.Application.Queries.AnalysisProfileQueries;
using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;

namespace Reshape.BusinessManagementService.API.Infrastructure.AutoMapperConfigs
{
    public class AnalysisProfileMapping : Profile
    {
        public AnalysisProfileMapping()
        {
            CreateMap<AnalysisProfileViewModel, AnalysisProfileDTO>().ReverseMap();
            CreateMap<AnalysisProfileViewModel, AnalysisProfile>().ReverseMap();
            CreateMap<AnalysisProfileDTO, AnalysisProfile>().ReverseMap();

            CreateMap<MediaType, MediaTypeDTO>().ReverseMap();
            CreateMap<MediaType, MediaTypeViewModel>().ReverseMap();
            CreateMap<MediaTypeViewModel, MediaTypeDTO>().ReverseMap();

            CreateMap<ScriptFile, ScriptFileDTO>().ReverseMap();
            CreateMap<ScriptFile, ScriptFileViewModel>().ReverseMap();
            CreateMap<ScriptFileViewModel, ScriptFileDTO>().ReverseMap();

            CreateMap<ScriptParametersFile, ScriptParametersFileDTO>().ReverseMap();
            CreateMap<ScriptParametersFile, ScriptParametersFileViewModel>().ReverseMap();
            CreateMap<ScriptParametersFileViewModel, ScriptParametersFileDTO>().ReverseMap();
        }
    }
}