using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reshape.AccountService.API.Application.Queries.AccountQueries
{
    public interface IAccountQueries
    {
        Task<IEnumerable<AccountViewModel>> GetAllAccountsAsync();
        Task<AccountViewModel> GetAccountById(Guid id);
    }
}