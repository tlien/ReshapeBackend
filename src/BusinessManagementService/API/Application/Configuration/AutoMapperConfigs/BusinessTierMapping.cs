using AutoMapper;

using Reshape.BusinessManagementService.API.Application.Commands;
using Reshape.BusinessManagementService.API.Application.Queries.BusinessTierQueries;
using Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;

namespace Reshape.BusinessManagementService.API.Configuration.AutoMapperConfigs
{
    public class BusinessTierMapping : Profile
    {
        public BusinessTierMapping()
        {
            CreateMap<BusinessTier, BusinessTierDTO>().ReverseMap();
            CreateMap<BusinessTier, BusinessTierViewModel>().ReverseMap();
            CreateMap<BusinessTierDTO, BusinessTierViewModel>().ReverseMap();
        }
    }
}