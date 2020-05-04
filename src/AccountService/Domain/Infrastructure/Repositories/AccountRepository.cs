using System.Threading.Tasks;
using AccountService.Domain.AggregatesModel.AccountAggregate;
using Common.SeedWork;

namespace AccountService.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        IUnitOfWork IRepository<Account>.UnitOfWork => throw new System.NotImplementedException();

        public Account Add(Account account)
        {
            return account;
        }

        public void Update(Account account)
        {

        }

        public void Remove(Account account)
        {

        }

        public Task<Account> GetAsync(Account account)
        {
            throw new System.NotImplementedException();
        }

        Task<Account> IAccountRepository.GetAsync(Account account)
        {
            throw new System.NotImplementedException();
        }
    }
}