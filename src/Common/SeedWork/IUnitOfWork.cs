using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Reshape.Common.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDbContextTransaction GetCurrentTransaction();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        int SaveChanges();
    }
}