using Microsoft.EntityFrameworkCore;

namespace Reshape.Common.DevelopmentTools
{
    /// <summary>
    /// A contract to be implemented by any DbContext based class with an associated database seeder.
    ///
    /// **THIS IS ONLY FOR DEVELOPMENT USE!**
    /// </summary>
    /// <typeparam name="TDbContext">The DbContext to be seeded</typeparam>
    public interface ISeeder<TDbContext> where TDbContext : DbContext
    {
        /// <summary>
        /// Seed database with predefined data.
        /// </summary>
        /// <returns></returns>
        TDbContext AddSeedData();
    }
}