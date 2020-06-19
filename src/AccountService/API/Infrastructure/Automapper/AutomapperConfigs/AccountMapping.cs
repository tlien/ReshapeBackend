using System.Linq;
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
            CreateMap<Account, AccountViewModel>()
                .ForMember(d => d.Features,
                            opts => opts.MapFrom(s => s.AccountFeatures.Select(aap => aap.Feature)))
                .ForMember(d => d.AnalysisProfiles,
                            opts => opts.MapFrom(s => s.AccountAnalysisProfiles.Select(af => af.AnalysisProfile)));
            CreateMap<Address, AddressViewModel>();
            CreateMap<ContactDetails, ContactDetailsViewModel>();
            CreateMap<BusinessTier, BusinessTierViewModel>();
            CreateMap<Feature, FeatureViewModel>();
            CreateMap<AnalysisProfile, AnalysisProfileViewModel>();
        }
    }
}