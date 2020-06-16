using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using IdentityServer4.EntityFramework.DbContexts;

namespace Reshape.IdentityService.Infrastructure
{
    public class PersistedGrantDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext CreateDbContext(string[] args)
        {
            var options = new IdentityServer4.EntityFramework.Options.OperationalStoreOptions();
            var optionsBuilder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
            optionsBuilder.UseNpgsql(".", opt =>
            {
                opt.MigrationsAssembly(GetType().Assembly.GetName().Name);
                opt.EnableRetryOnFailure();
            });

            return new PersistedGrantDbContext(optionsBuilder.Options, options);
        }
    }

    // public class ConfigurationDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
    // {
    //     public ConfigurationDbContext CreateDbContext(string[] args)
    //     {
    //         var options = new IdentityServer4.EntityFramework.Options.ConfigurationStoreOptions();
    //         var optionsBuilder = new DbContextOptionsBuilder<ConfigurationDbContext>();
    //         optionsBuilder.UseNpgsql(".", opt =>
    //         {
    //             opt.MigrationsAssembly(GetType().Assembly.GetName().Name);
    //             opt.EnableRetryOnFailure();
    //         });

    //         return new ConfigurationDbContext(optionsBuilder.Options, options);
    //     }
    // }
}