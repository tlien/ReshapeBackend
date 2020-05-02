using AccountService.Domain.AggregatesModel.AccountAggregate;

namespace AccountService.Infrastructure.Repositories {
    public class AccountRepository : IAccountRepository {
        private readonly AccountContext _context;

        public IUnitOfWork UnitOfWork {
            get {
                return _context;
            }
        }
        
        public Account Add(Account account) {
            return account;
        }

        public void Update(Account account) {

        }

        public void Remove(Account account) {

        }

        public Task<Account> GetAsync(Account account) {
            return account;
        }
    }
}