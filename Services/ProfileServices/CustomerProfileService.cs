using AutoMapper;
using CustomerManagementService.Data.Models;
using CustomerManagementService.Dtos;
using CustomerManagementService.Repository.ProfilesRepository;
using CustomerManagementService.Services.CustomerServices;

namespace CustomerManagementService.Services.ProfileServices;

public class CustomerProfileService : ICustomerProfileService
{
    private readonly IMapper _mapper;
    private readonly ICustomerProfileRepository _customerProfileRepository;


    public CustomerProfileService(IMapper mapper,
        ICustomerProfileRepository customerProfileRepository)
    {
        _mapper = mapper;
        _customerProfileRepository = customerProfileRepository;
    }

    public async Task<CustomerProfileDto> GetProfileByCustomerIdAsync(Guid? customerId)
    {
        if (customerId == null)
        {
            throw new ArgumentNullException(nameof(customerId));
        }
        var profile = await _customerProfileRepository.GetProfileByCustomerIdAsync(customerId);
        if (profile == null)
        {
            throw new KeyNotFoundException("Profile not found.");
        }
        var profileDto = _mapper.Map<CustomerProfileDto>(profile);
        return profileDto;
    }

    public async Task CreateProfileAsync(CustomerProfileDto customerProfileDto, Guid? customerId)
    {
        if(customerId == null) throw new ArgumentNullException(nameof(customerId));
        var profile = _mapper.Map<CustomerProfile>(customerProfileDto);
        profile.CustomerId = customerId;
        await _customerProfileRepository.AddProfileAsync(profile);
    }

    public async Task UpdateProfileByCustomerIdAsync(Guid? customerId, CustomerProfileDto customerProfileDto)
    {
        if(customerId == null) throw new ArgumentNullException(nameof(customerId));

        var profileToUpdate = await _customerProfileRepository.GetProfileByCustomerIdAsync(customerId);
        if (profileToUpdate == null)
        {
            throw new KeyNotFoundException($"Profile with ID {customerId} not found.");
        }

        _mapper.Map(customerProfileDto, profileToUpdate);
        await _customerProfileRepository.UpdateProfileByCustomerIdAsync(profileToUpdate);
    }
}