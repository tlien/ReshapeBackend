using AutoMapper;
using BusinessManagementService.API.Application.Commands;
using BusinessManagementService.API.Application.Queries.FeatureQueries;
using BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;
using static BusinessManagementService.API.Application.Commands.CreateFeatureCommandHandler;

namespace BusinessManagementService.API.Configuration.AutoMapperConfigs
{
    public class FeatureMapping : Profile
    {
        public FeatureMapping()
        {
            CreateMap<Feature, FeatureDTO>().ReverseMap();
            CreateMap<Feature, FeatureViewModel>().ReverseMap();
            CreateMap<FeatureDTO, FeatureViewModel>().ReverseMap();
        }
    }
}