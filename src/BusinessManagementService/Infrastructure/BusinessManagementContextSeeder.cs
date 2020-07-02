using System;
using System.Collections.Generic;
using System.Linq;
using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;
using Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;

namespace Reshape.BusinessManagementService.Infrastructure
{
    internal static class BusinessManagementContextSeeder
    {
        internal static BusinessManagementContext AddSeedData(this BusinessManagementContext context)
        {
            // Check (naively) if BusinessTiers have already been seeded
            if (!context.BusinessTiers.Any())
            {
                var businessTiers = new List<BusinessTier>() {
                    new BusinessTier(Guid.Parse("0c69921b-afa7-4eba-a69f-aaef3a5c3f5b"), "Paid", "Long and informative description full of details and stuff woah!", 100.00m),
                    new BusinessTier(Guid.Parse("2992a45a-9b79-4eee-aa30-240ccefe4ec2"), "Free", "Long and informative description full of details and stuff woah!", 0.00m)
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
                var mediaType1 = new MediaType(Guid.Parse("523db2b5-d266-49e9-a318-d65479933b3a"), "MediaType1");
                var mediaType2 = new MediaType(Guid.Parse("c6c599aa-7e81-4e3c-8ea2-b2621b3bed5c"), "MediaType2");
                var mediaType3 = new MediaType(Guid.Parse("9ba7606b-d8ce-4720-b2b7-ddc226b80a53"), "MediaType3");

                var temp1 = new AnalysisProfile(Guid.Parse("40b8cc10-8cec-40a8-98e5-30fcca5a847c"), "AnalysisProfile1", "Long and informative description full of details and stuff woah!", 100.00m);
                temp1.SetScriptFile(new ScriptFile(Guid.Parse("9ad053df-075e-4372-9367-f810a8eacca9"), "ScriptFile1", "Long and informative description full of details and stuff woah!", "print(\"hello world!\")"));
                temp1.SetScriptParametersFile(new ScriptParametersFile(Guid.Parse("b54ad278-9c45-4c0e-91b3-32c8495920a4"), "ScriptParametersFile1", "Long and informative description full of details and stuff woah!", "{\"key\":\"value\"}"));
                temp1.SetMediaType(mediaType1);

                var temp2 = new AnalysisProfile(Guid.Parse("4218b6af-6429-4ccb-9e96-b8403fbce655"), "AnalysisProfile2", "Long and informative description full of details and stuff woah!", 100.00m);
                temp2.SetScriptFile(new ScriptFile(Guid.Parse("e1c2178c-2d99-46f5-b0fe-3d71793eec1b"), "ScriptFile2", "Long and informative description full of details and stuff woah!", "print(\"hello world!\")"));
                temp2.SetScriptParametersFile(new ScriptParametersFile(Guid.Parse("1796d543-0ac2-4ca9-90fa-66f48452d234"), "ScriptParametersFile2", "Long and informative description full of details and stuff woah!", "{\"key\":\"value\"}"));
                temp2.SetMediaType(mediaType2);

                var temp3 = new AnalysisProfile(Guid.Parse("ea26702e-5aa0-49dd-8a16-0d16f6bfb5ab"), "AnalysisProfile3", "Long and informative description full of details and stuff woah!", 100.00m);
                temp3.SetScriptFile(new ScriptFile(Guid.Parse("8393a386-dbbc-4122-b0b0-442de6d05f7c"), "ScriptFile3", "Long and informative description full of details and stuff woah!", "print(\"hello world!\")"));
                temp3.SetScriptParametersFile(new ScriptParametersFile(Guid.Parse("e7117473-6b0d-4200-9004-4186a4fd7e79"), "ScriptParametersFile3", "Long and informative description full of details and stuff woah!", "{\"key\":\"value\"}"));
                temp3.SetMediaType(mediaType3);

                var temp4 = new AnalysisProfile(Guid.Parse("727f66cc-3161-4796-9136-dcef15f2d661"), "AnalysisProfile4", "Long and informative description full of details and stuff woah!", 100.00m);
                temp4.SetScriptFile(new ScriptFile(Guid.Parse("fa433ebd-ab54-47fd-a936-d250d44fb52c"), "ScriptFile4", "Long and informative description full of details and stuff woah!", "print(\"hello world!\")"));
                temp4.SetScriptParametersFile(new ScriptParametersFile(Guid.Parse("70a27cca-6b45-4ca6-bdee-e5c6aa8ab318"), "ScriptParametersFile4", "Long and informative description full of details and stuff woah!", "{\"key\":\"value\"}"));
                temp4.SetMediaType(mediaType1);

                var temp5 = new AnalysisProfile(Guid.Parse("55776aa1-68b8-48df-8a37-c3f3cd15db94"), "AnalysisProfile5", "Long and informative description full of details and stuff woah!", 100.00m);
                temp5.SetScriptFile(new ScriptFile(Guid.Parse("a022e3a0-96a3-44ec-b5a8-70be7afbbdb6"), "ScriptFile5", "Long and informative description full of details and stuff woah!", "print(\"hello world!\")"));
                temp5.SetScriptParametersFile(new ScriptParametersFile(Guid.Parse("b062f0b7-6ff8-4dbf-8752-0c0d8f5e9bae"), "ScriptParametersFile5", "Long and informative description full of details and stuff woah!", "{\"key\":\"value\"}"));
                temp5.SetMediaType(mediaType2);

                var temp6 = new AnalysisProfile(Guid.Parse("d90c7d0e-6c4e-4baa-9f04-076215db86fa"), "AnalysisProfile6", "Long and informative description full of details and stuff woah!", 100.00m);
                temp6.SetScriptFile(new ScriptFile(Guid.Parse("ff9db33b-2529-4e1e-97b6-f57d97e9de2e"), "ScriptFile6", "Long and informative description full of details and stuff woah!", "print(\"hello world!\")"));
                temp6.SetScriptParametersFile(new ScriptParametersFile(Guid.Parse("fb32ad84-eccf-4470-8c61-95828e5bbb5a"), "ScriptParametersFile6", "Long and informative description full of details and stuff woah!", "{\"key\":\"value\"}"));
                temp6.SetMediaType(mediaType3);

                context.AddRange(temp1, temp2, temp3, temp4, temp5, temp6);
            }

            context.SaveChanges();

            return context;
        }
    }
}
