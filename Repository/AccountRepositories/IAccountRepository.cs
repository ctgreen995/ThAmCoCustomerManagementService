using CustomerManagementService.Data.Models;
using CustomerManagementService.Models;

namespace CustomerManagementService.Repository.AccountRepositories;

public interface IAccountRepository
{
    Task AddAccountAsync(Account account, Guid customerId);
    Task<Account> GetAccountByCustomerIdAsync(Guid customerId);
}