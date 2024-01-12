using CustomerManagementService.Dtos;

namespace CustomerManagementService.Services.RequestDeleteCustomerSevices;

public interface IRequestDeleteCustomerService
{
    Task<HttpResponseMessage> RequestDeleteCustomerAsync(CustomerDto customer);
}
