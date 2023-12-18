using CustomerManagementService.Data.Models;
using CustomerManagementService.Models;

namespace CustomerManagementService.Repository.AccountRepositories;

public interface ICustomerAccountRepository
{
    Task AddAccountAsync(CustomerAccount customerAccount);
    Task<CustomerAccount?> GetAccountByCustomerIdAsync(Guid customerId);
    Task UpdateAccountByCustomerIdAsync(CustomerAccount customerAccount);
    Task DeleteAccountAsync(Guid customerId);
}