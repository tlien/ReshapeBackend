using AutoMapper;
using BusinessManagementService.API.Application.Commands;
using BusinessManagementService.API.Application.Queries.BusinessTierQueries;
using BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;

namespace BusinessManagementService.API.Configuration.AutoMapperConfigs
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