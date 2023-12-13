using CustomerManagementService.Models;

namespace CustomerManagementService.Repository.CustomerRepositories;

public interface ICustomerRepository
{
    Task<Guid> AddCustomerAsync(string authId);
    Task<Customer?> GetCustomerByAuthIdAsync(string id);
}