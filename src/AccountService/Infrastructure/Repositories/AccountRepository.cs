using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Common.SeedWork;
using AccountService.Domain.AggregatesModel.AccountAggregate;

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

        public AccountRepository(AccountContext ctx)
        {
            _context = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        public Account Add(Account account)
        {
            return _context.Accounts.Add(account).Entity;
        }

        public void Update(Account account)
        {
            _context.Entry(account).State = EntityState.Modified;
        }

        public async Task<Account> GetAsync(Guid accountId)
        {
            var account = await _context
                                    .Accounts
                                    .Include(x => x.address)
                                    .Include(x => x.contactDetails)
                                    .Include(x => x.features)
                                    .FirstOrDefaultAsync(acc => acc.Id == accountId);

            // try to get account from local uncommitted entities
            if (account == null)
            {
                account = _context
                            .Accounts
                            .Local
                            .FirstOrDefault(acc => acc.Id == accountId);
            }

            // TODO: Figure out if these actually need to be loaded at this point
            if (account != null)
            {
                await _context.Entry(account).Reference(x => x.address).LoadAsync();
                await _context.Entry(account).Reference(x => x.contactDetails).LoadAsync();
                await _context.Entry(account).Collection(x => x.features).LoadAsync();
            }

            return account;
        }
    }
}