using AutoMapper;
using Reshape.BusinessManagementService.API.Application.Commands;
using Reshape.BusinessManagementService.API.Application.Queries.FeatureQueries;
using Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;
using static Reshape.BusinessManagementService.API.Application.Commands.CreateFeatureCommandHandler;

namespace Reshape.BusinessManagementService.API.Configuration.AutoMapperConfigs
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