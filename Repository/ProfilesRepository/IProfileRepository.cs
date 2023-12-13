using AutoMapper;

namespace CustomerManagementService.Repository.ProfilesRepository;

public interface IProfileRepository
{
    Task AddProfileAsync(Profile profile, Guid customerId);
    Task<Profile> GetProfileByCustomerIdAsync(Guid customerId);
}