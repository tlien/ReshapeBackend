using Microsoft.EntityFrameworkCore;

namespace Reshape.Common.DevelopmentTools
{
    public interface ISeeder<T> where T : DbContext
    {
        T AddSeedData();
    }
}