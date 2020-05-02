using System.Threading;
using System.Threading.Tasks;
using AccountService.Domain.AggregatesModel.AccountAggregate;
using Common.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Infrastructure {
    public class AccountContext : DbContext, IUnitOfWork {
        public const string DEFAULT_SCHEMA = "account";
        public DbSet<Account> accounts { get; set; }

        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}