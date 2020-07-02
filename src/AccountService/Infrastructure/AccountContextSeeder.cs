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
            var businessTiers = new List<BusinessTier>() {
                new BusinessTier(Guid.Parse("0c69921b-afa7-4eba-a69f-aaef3a5c3f5b"), "Paid", "Long and informative description full of details and stuff woah!", 100.00m),
                new BusinessTier(Guid.Parse("2992a45a-9b79-4eee-aa30-240ccefe4ec2"), "Free", "Long and informative description full of details and stuff woah!", 0.00m)
            };

            // Check (naively) if BusinessTiers have already been seeded
            if (!context.BusinessTiers.Any())
            {
                context.AddRange(businessTiers);
            }

            var features = new List<Feature>() {
                new Feature(Guid.Parse("17bcd22f-41cf-44d1-894b-6634caf7b489"), "Feature1", "Long and informative description full of details and stuff woah!", 100.00m),
                new Feature(Guid.Parse("59b06dcc-8108-4233-b5d2-94fc93dd2455"), "Feature2", "Long and informative description full of details and stuff woah!", 100.00m),
                new Feature(Guid.Parse("36d8e579-e27a-43e4-b6c5-72fb7a55ce08"), "Feature3", "Long and informative description full of details and stuff woah!", 100.00m),
                new Feature(Guid.Parse("088ad0ab-57b1-48df-ac51-9c9bff608a72"), "Feature4", "Long and informative description full of details and stuff woah!", 100.00m)
            };

            // Check (naively) if features have already been seeded
            if (!context.Features.Any())
            {
                context.AddRange(features);
            }

            var analysisProfiles = new List<AnalysisProfile>() {
                new AnalysisProfile(Guid.Parse("40b8cc10-8cec-40a8-98e5-30fcca5a847c"), "AnalysisProfile1", "Long and informative description full of details and stuff woah!", 100.00m),
                new AnalysisProfile(Guid.Parse("4218b6af-6429-4ccb-9e96-b8403fbce655"), "AnalysisProfile2", "Long and informative description full of details and stuff woah!", 100.00m),
                new AnalysisProfile(Guid.Parse("ea26702e-5aa0-49dd-8a16-0d16f6bfb5ab"), "AnalysisProfile3", "Long and informative description full of details and stuff woah!", 100.00m),
                new AnalysisProfile(Guid.Parse("727f66cc-3161-4796-9136-dcef15f2d661"), "AnalysisProfile4", "Long and informative description full of details and stuff woah!", 100.00m),
                new AnalysisProfile(Guid.Parse("55776aa1-68b8-48df-8a37-c3f3cd15db94"), "AnalysisProfile5", "Long and informative description full of details and stuff woah!", 100.00m),
                new AnalysisProfile(Guid.Parse("d90c7d0e-6c4e-4baa-9f04-076215db86fa"), "AnalysisProfile6", "Long and informative description full of details and stuff woah!", 100.00m)
            };

            // Check (naively) if AnalysisProfiles have already been seeded
            if (!context.AnalysisProfiles.Any())
            {
                context.AddRange(analysisProfiles);
            }

            // Check(naively) if Accounts have already been seeded
            if (!context.Accounts.Any())
            {
                var account1 = new Account(Guid.Parse("bec823e4-aced-4b92-9442-70c2f32c65f9"));
                account1.SetAddress(new Address("One Haxxor Way", "1337", "Heidelberg", "69118", "Germany"));
                account1.SetContactDetails(new ContactDetails("Don Keigh", "818727", "DoKeigh@email.com"));
                account1.SetBusinessTier(businessTiers[1]);
                account1.AddFeature(features[2]);
                account1.AddFeature(features[3]);
                account1.AddAnalysisProfile(analysisProfiles[1]);
                account1.AddAnalysisProfile(analysisProfiles[3]);
                account1.AddAnalysisProfile(analysisProfiles[5]);

                var account2 = new Account(Guid.Parse("74c20cbc-9e0c-4cef-8325-27b8a26a64b1"));
                account2.SetAddress(new Address("One Hacker Way", "1337", "Heidelberg", "69118", "Germany"));
                account2.SetContactDetails(new ContactDetails("Bob Smith", "88421113", "BobSmith@email.com"));
                account2.SetBusinessTier(businessTiers[0]);
                account2.AddFeature(features[0]);
                account2.AddFeature(features[1]);
                account2.AddFeature(features[2]);
                account2.AddFeature(features[3]);
                account2.AddAnalysisProfile(analysisProfiles[0]);
                account2.AddAnalysisProfile(analysisProfiles[1]);
                account2.AddAnalysisProfile(analysisProfiles[2]);
                account2.AddAnalysisProfile(analysisProfiles[3]);
                account2.AddAnalysisProfile(analysisProfiles[4]);
                account2.AddAnalysisProfile(analysisProfiles[5]);

                context.AddRange(account1, account2);
            }

            context.SaveChanges();

            return context;
        }
    }
}
