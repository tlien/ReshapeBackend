using AutoMapper;

using Reshape.BusinessManagementService.API.Application.Commands;
using Reshape.BusinessManagementService.API.Application.Queries.FeatureQueries;
using Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;

namespace Reshape.BusinessManagementService.API.Infrastructure.AutoMapperConfigs
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