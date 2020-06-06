using AutoMapper;

using Reshape.AccountService.API.Application.Queries.AccountAdditionsQueries;
using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Infrastructure.AutoMapper
{
    public class AccountAdditionsMapping : Profile
    {
        public AccountAdditionsMapping()
        {
            // For CQRS Queries, no reverseMap needed
            CreateMap<AnalysisProfile, AnalysisProfileViewModel>();
            CreateMap<BusinessTier, BusinessTierViewModel>();
            CreateMap<Feature, FeatureViewModel>();
        }
    }
}