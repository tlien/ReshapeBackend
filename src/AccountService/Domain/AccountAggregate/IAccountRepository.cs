using System;
using System.Threading.Tasks;
using Reshape.Common.SeedWork;

namespace Reshape.AccountService.Domain.AggregatesModel.AccountAggregate
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account Add(Account account);
        void Update(Account account);
        Task<Account> GetAsync(Guid accountId);
    }
}