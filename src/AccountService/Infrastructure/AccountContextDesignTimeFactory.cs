

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Reshape.AccountService.Infrastructure
{
    /// <summary>
    /// Allows webhosting extension to migrate database at design time.
    /// The factory is accessed simply by being in the same project root or namespace as the <c>DbContext</c>
    /// it is producing, hence no code references to <c>BusinessManagementContextFactory</c>.
    /// </summary>
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