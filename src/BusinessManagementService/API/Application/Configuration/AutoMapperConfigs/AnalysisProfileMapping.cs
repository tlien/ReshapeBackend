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
            CreateMap<AnalysisProfileRequiredFeatureViewModel, AnalysisProfileRequiredFeatureDTO>().ReverseMap();
            CreateMap<AnalysisProfileRequiredFeatureViewModel, AnalysisProfileRequiredFeature>().ReverseMap();
        }
    }
}