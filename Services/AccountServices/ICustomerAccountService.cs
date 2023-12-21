using CustomerManagementService.Data.Models;
using CustomerManagementService.Dtos;

namespace CustomerManagementService.Services.AccountServices;

public interface ICustomerAccountService
{
    Task UpdateAccountByCustomerIdAsync(Guid? customerId, CustomerAccountDto customerAccountDto);
    Task<CustomerAccountDto> GetAccountByCustomerIdAsync(Guid? customerId);
    Task CreateAccountAsync(CustomerAccountDto customerAccount, Guid? customerId);
}