using CustomerManagementService.Dtos;

namespace CustomerManagementService.Services.CustomerServices;

public interface ICustomerService
{
    Task<CustomerDto?> GetCustomerByIdAsync(string id);
    Task<Guid?> CreateCustomerAsync(CustomerDto customerDto);
    Task DeleteCustomerAsync(string id);
}