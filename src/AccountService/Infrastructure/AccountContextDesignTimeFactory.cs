

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Reshape.AccountService.Infrastructure
{
    // Since Program has been heavily altered, meaning EF can't find the dbcontext during design time using that convention.
    // Providing a factory implementing IDesignTimeDbContextFactory solves this in a graceful manner.
    public class AccountContextFactory : IDesignTimeDbContextFactory<AccountContext>
    {
        public AccountContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AccountContext>();
            optionsBuilder.UseNpgsql(".", opt =>
            {
                opt.MigrationsAssembly(GetType().Assembly.GetName().Name);
                opt.EnableRetryOnFailure();
            })
            .UseSnakeCaseNamingConvention();

            return new AccountContext(optionsBuilder.Options);
        }
    }
}