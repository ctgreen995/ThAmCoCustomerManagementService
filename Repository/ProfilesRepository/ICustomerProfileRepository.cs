using AutoMapper;
using CustomerManagementService.Data.Models;

namespace CustomerManagementService.Repository.ProfilesRepository;

public interface ICustomerProfileRepository
{
    Task AddProfileAsync(CustomerProfile profile);
    Task<CustomerProfile?> GetProfileByCustomerIdAsync(Guid customerId);
    Task UpdateProfileByCustomerIdAsync(CustomerProfile profile);
    Task DeleteProfileAsync(Guid customerId);
}