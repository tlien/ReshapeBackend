using AutoMapper;
using Reshape.BusinessManagementService.API.Configuration.AutoMapperConfigs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reshape.BusinessManagementService.API.Configuration
{
    public static class AutoMapperConfig
    {
        public static IMapper CreateMapper() => new MapperConfiguration(cfg =>
        {
            var profiles = typeof(AnalysisProfileMapping).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));
            foreach (var profile in profiles)
            {
                cfg.AddProfile(Activator.CreateInstance(profile) as Profile);
            }
        }
        ).CreateMapper();
    }
}
