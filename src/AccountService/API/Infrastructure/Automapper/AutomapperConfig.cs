using System;
using System.Linq;
using AutoMapper;

namespace Reshape.AccountService.API.Infrastructure.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static IMapper CreateMapper() => new MapperConfiguration(cfg =>
        {
            var profiles = typeof(AccountMapping).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));
            foreach (var profile in profiles)
            {
                cfg.AddProfile(Activator.CreateInstance(profile) as Profile);
            }
        }
        ).CreateMapper();
    }
}