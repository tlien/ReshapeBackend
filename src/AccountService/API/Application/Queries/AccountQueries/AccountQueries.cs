using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;

using Reshape.AccountService.Infrastructure;

namespace Reshape.AccountService.API.Application.Queries.AccountQueries
{
    /// <summary>
    /// Holds database queries to get <c>Accounts</c>.
    /// </summary>
    public class AccountQueries : IAccountQueries
    {
        private readonly AccountContext _context;
        private readonly IMapper _mapper;

        public AccountQueries(AccountContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets a specific <c>Account</c> by its id.
        /// </summary>
        /// <param name="id">The id of the <c>Account</c></param>
        /// <returns>A task that returns the <c>Account</c> when awaited.</returns>
        public async Task<AccountViewModel> GetAccountById(Guid id)
        {
            return await _context
                            .Accounts
                            .AsNoTracking()
                            .ProjectTo<AccountViewModel>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync(a => a.Id == id);
        }

        /// <summary>
        /// Gets a list of all <c>Accounts</c>.
        /// </summary>
        /// <returns>A task that returns list of <c>Accounts</c> when awaited.</returns>
        public async Task<IEnumerable<AccountViewModel>> GetAllAccountsAsync()
        {
            return await _context
                            .Accounts
                            .AsNoTracking()
                            .ProjectTo<AccountViewModel>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }
    }
}