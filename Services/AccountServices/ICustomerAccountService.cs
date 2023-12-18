using CustomerManagementService.Data.Models;
using CustomerManagementService.Dtos;

namespace CustomerManagementService.Services.AccountServices;

public interface ICustomerAccountService
{
    Task UpdateAccountByAuthIdAsync(string authId, CustomerAccountDto customerAccountDto);
    Task<CustomerAccountDto> GetAccountByCustomerIdAsync(Guid customerId);
    Task CreateAccountAsync(CustomerAccountDto customerAccount, Guid customerId);
    Task DeleteAccountAsync(Guid customerId);
}