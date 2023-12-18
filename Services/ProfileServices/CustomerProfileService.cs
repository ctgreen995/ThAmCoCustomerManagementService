using AutoMapper;
using CustomerManagementService.Data.Models;
using CustomerManagementService.Dtos;
using CustomerManagementService.Repository.ProfilesRepository;
using CustomerManagementService.Services.CustomerServices;

namespace CustomerManagementService.Services.ProfileServices;

public class CustomerProfileService : ICustomerProfileService
{
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;
    private readonly ICustomerProfileRepository _customerProfileRepository;


    public CustomerProfileService(ICustomerService customerService, IMapper mapper,
        ICustomerProfileRepository customerProfileRepository)
    {
        _customerService = customerService;
        _mapper = mapper;
        _customerProfileRepository = customerProfileRepository;
    }

    public async Task<CustomerProfileDto> GetProfileByCustomerIdAsync(Guid customerId)
    {
        var profile = await _customerProfileRepository.GetProfileByCustomerIdAsync(customerId);
        var profileDto = _mapper.Map<CustomerProfileDto>(profile);
        return profileDto;
    }

    public async Task CreateProfileAsync(CustomerProfileDto customerProfileDto, Guid customerId)
    {
        var profile = _mapper.Map<CustomerProfile>(customerProfileDto);
        profile.CustomerId = customerId;
        await _customerProfileRepository.AddProfileAsync(profile);
    }

    public Task UpdateProfileByAuthIdAsync(string authId, CustomerProfileDto customerProfileDto)
    {
        var customerId = _customerService.GetCustomerIdByAuthIdAsync(authId);
        if (customerId == null)
        {
            throw new Exception("Customer not found.");
        }

        var profile = _mapper.Map<CustomerProfile>(customerProfileDto);
        return _customerProfileRepository.UpdateProfileByCustomerIdAsync(profile);
    }

    public async Task DeleteProfileByCustomerIdAsync(string id)
    {
        var customerId = await _customerService.GetCustomerIdByAuthIdAsync(id);
        if (customerId == null)
        {
            throw new Exception("Customer not found.");
        }

        await _customerProfileRepository.DeleteProfileAsync(customerId.Value);
    }

    public async Task DeleteProfileAsync(Guid customerId)
    {
        await _customerProfileRepository.DeleteProfileAsync(customerId);
    }
}