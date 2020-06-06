using System;
using System.Collections.Generic;
using System.Linq;

using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.Infrastructure
{
    internal static class AccountContextSeeder
    {
        internal static AccountContext AddSeedData(this AccountContext context)
        {
            // Check (naively) if BusinessTiers have already been seeded
            if (!context.BusinessTiers.Any())
            {
                var businessTiers = new List<BusinessTier>() {
                    new BusinessTier(Guid.Parse("0c69921b-afa7-4eba-a69f-aaef3a5c3f5b"), "BusinessTier1", "Long and informative description full of details and stuff woah!", 100.00m),
                    new BusinessTier(Guid.Parse("2992a45a-9b79-4eee-aa30-240ccefe4ec2"), "BusinessTier2", "Long and informative description full of details and stuff woah!", 100.00m),
                    new BusinessTier(Guid.Parse("af22db79-9f91-4a0f-b50a-5e935dab6c55"), "BusinessTier3", "Long and informative description full of details and stuff woah!", 100.00m)
                };
                context.AddRange(businessTiers);
            }

            // Check (naively) if features have already been seeded
            if (!context.Features.Any())
            {
                var features = new List<Feature>() {
                    new Feature(Guid.Parse("17bcd22f-41cf-44d1-894b-6634caf7b489"), "Feature1", "Long and informative description full of details and stuff woah!", 100.00m),
                    new Feature(Guid.Parse("59b06dcc-8108-4233-b5d2-94fc93dd2455"), "Feature2", "Long and informative description full of details and stuff woah!", 100.00m),
                    new Feature(Guid.Parse("36d8e579-e27a-43e4-b6c5-72fb7a55ce08"), "Feature3", "Long and informative description full of details and stuff woah!", 100.00m),
                    new Feature(Guid.Parse("088ad0ab-57b1-48df-ac51-9c9bff608a72"), "Feature4", "Long and informative description full of details and stuff woah!", 100.00m)
                };
                context.AddRange(features);
            }

            // Check (naively) if AnalysisProfiles have already been seeded
            if (!context.AnalysisProfiles.Any())
            {
                var analysisProfiles = new List<AnalysisProfile>() {
                    new AnalysisProfile(Guid.Parse("40b8cc10-8cec-40a8-98e5-30fcca5a847c"), "AnalysisProfile1", "Long and informative description full of details and stuff woah!", 100.00m),
                    new AnalysisProfile(Guid.Parse("4218b6af-6429-4ccb-9e96-b8403fbce655"), "AnalysisProfile2", "Long and informative description full of details and stuff woah!", 100.00m),
                    new AnalysisProfile(Guid.Parse("ea26702e-5aa0-49dd-8a16-0d16f6bfb5ab"), "AnalysisProfile3", "Long and informative description full of details and stuff woah!", 100.00m),
                    new AnalysisProfile(Guid.Parse("727f66cc-3161-4796-9136-dcef15f2d661"), "AnalysisProfile4", "Long and informative description full of details and stuff woah!", 100.00m),
                    new AnalysisProfile(Guid.Parse("55776aa1-68b8-48df-8a37-c3f3cd15db94"), "AnalysisProfile5", "Long and informative description full of details and stuff woah!", 100.00m),
                    new AnalysisProfile(Guid.Parse("d90c7d0e-6c4e-4baa-9f04-076215db86fa"), "AnalysisProfile6", "Long and informative description full of details and stuff woah!", 100.00m)
                };
                context.AddRange(analysisProfiles);
            }

            // Check (naively) if Accounts have already been seeded
            // if (!context.Accounts.Any())
            // {
            //     // context.Add(new Account())
            // }

            context.SaveChanges();

            return context;
        }
    }
}