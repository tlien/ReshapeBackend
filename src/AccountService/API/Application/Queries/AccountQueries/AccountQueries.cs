using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;

using Reshape.AccountService.Infrastructure;

namespace Reshape.AccountService.API.Application.Queries.AccountQueries
{
    public class AccountQueries : IAccountQueries
    {
        private readonly AccountContext _context;
        private readonly IMapper _mapper;

        public AccountQueries(AccountContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<AccountViewModel> GetAccountById(Guid id)
        {
            return await _context
                            .Accounts
                            .ProjectTo<AccountViewModel>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<AccountViewModel>> GetAllAccountsAsync()
        {
            return await _context
                            .Accounts
                            .ProjectTo<AccountViewModel>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }
    }
}