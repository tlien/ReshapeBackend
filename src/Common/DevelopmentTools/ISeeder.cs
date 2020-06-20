using Microsoft.EntityFrameworkCore;

namespace Reshape.Common.DevelopmentTools
{
    public interface ISeeder<TDbContext> where TDbContext : DbContext
    {
        TDbContext AddSeedData();
    }
}