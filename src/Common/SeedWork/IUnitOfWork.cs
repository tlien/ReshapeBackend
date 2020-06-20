using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Reshape.Common.SeedWork
{
    /// <summary>
    /// Contract used for repositories and dbcontexts
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IDbContextTransaction GetCurrentTransaction();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}