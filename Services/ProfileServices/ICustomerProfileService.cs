using CustomerManagementService.Dtos;

namespace CustomerManagementService.Services.ProfileServices;

public interface ICustomerProfileService
{
    Task<CustomerProfileDto> GetProfileByCustomerIdAsync(Guid? customerId);
    Task CreateProfileAsync(CustomerProfileDto customerProfileDto, Guid? customerId);
    Task UpdateProfileByCustomerIdAsync(Guid? customerId, CustomerProfileDto customerProfileDto);
}