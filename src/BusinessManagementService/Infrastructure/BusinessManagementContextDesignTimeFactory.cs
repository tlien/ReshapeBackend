
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Reshape.BusinessManagementService.Infrastructure
{
    /// <summary>
    /// Allows webhosting extension to migrate database at design time.
    /// The factory is accessed simply by being in the same project root or namespace as the <c>DbContext</c>
    /// it is producing, hence no code references to <c>BusinessManagementContextFactory</c>.
    /// </summary>
    public class BusinessManagementContextFactory : IDesignTimeDbContextFactory<BusinessManagementContext>
    {
        public BusinessManagementContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BusinessManagementContext>();

            optionsBuilder.UseNpgsql(".", options =>
                {
                    options.MigrationsAssembly(GetType().Assembly.GetName().Name);
                    options.EnableRetryOnFailure();
                }
            ).UseSnakeCaseNamingConvention();

            return new BusinessManagementContext(optionsBuilder.Options);
        }
    }
}