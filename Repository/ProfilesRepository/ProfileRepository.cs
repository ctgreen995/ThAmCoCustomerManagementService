using AutoMapper;

namespace CustomerManagementService.Repository.ProfilesRepository;

public class ProfileRepository : IProfileRepository
{
    public Task AddProfileAsync(Profile profile, Guid customerId)
    {
        throw new NotImplementedException();
    }

    public Task<Profile> GetProfileByCustomerIdAsync(Guid customerId)
    {
        throw new NotImplementedException();
    }
}