using AutoMapper;
using BusinessManagementService.API.Application.Queries.AnalysisProfileQueries;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using static BusinessManagementService.API.Application.Commands.CreateAnalysisProfileCommand;
using static BusinessManagementService.API.Application.Commands.CreateAnalysisProfileCommandHandler;

namespace BusinessManagementService.API.Configuration.AutoMapperConfigs
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