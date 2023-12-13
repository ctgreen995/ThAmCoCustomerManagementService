using CustomerManagementService.Data.Models;
using CustomerManagementService.Models;

namespace CustomerManagementService.Repository.AccountRepositories;

public class AccountRepository : IAccountRepository
{
    public Task AddAccountAsync(Account account, Guid customerId)
    {
        throw new NotImplementedException();
    }

    public Task<Account> GetAccountByCustomerIdAsync(Guid customerId)
    {
        throw new NotImplementedException();
    }
}