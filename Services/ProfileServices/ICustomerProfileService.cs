using CustomerManagementService.Dtos;

namespace CustomerManagementService.Services.ProfileServices;

public interface ICustomerProfileService
{
    Task<CustomerProfileDto> GetProfileByCustomerIdAsync(Guid customerId);
    Task CreateProfileAsync(CustomerProfileDto customerProfileDto, Guid customerId);
    Task UpdateProfileByAuthIdAsync(string authId, CustomerProfileDto customerProfileDto);
    Task DeleteProfileByCustomerIdAsync(string id);
    Task DeleteProfileAsync(Guid customerId);
}