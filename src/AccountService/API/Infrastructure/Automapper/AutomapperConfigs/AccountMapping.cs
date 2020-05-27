using AutoMapper;

using Reshape.AccountService.API.Application.Queries.AccountQueries;
using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.API.Infrastructure.AutoMapper
{
    public class AccountMapping : Profile
    {
        public AccountMapping()
        {
            // For CQRS Queries, no reverseMap needed
            CreateMap<Account, AccountViewModel>();
            CreateMap<Address, AddressViewModel>();
            CreateMap<ContactDetails, ContactDetailsViewModel>();
            CreateMap<BusinessTier, BusinessTierViewModel>();
            CreateMap<Feature, FeatureViewModel>();
            CreateMap<AnalysisProfile, AnalysisProfileViewModel>();
        }
    }
}