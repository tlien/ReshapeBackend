using Microsoft.EntityFrameworkCore;

namespace Reshape.Common.DbStuff
{
    public interface ISeeder<T> where T : DbContext
    {
        T AddSeedData();
    }
}