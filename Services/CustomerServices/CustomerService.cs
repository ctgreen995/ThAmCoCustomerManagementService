using AutoMapper;
using CustomerManagementService.Data.Models;
using CustomerManagementService.Dtos;
using CustomerManagementService.Models;
using CustomerManagementService.Repository.AccountRepositories;
using CustomerManagementService.Repository.CustomerRepositories;
using CustomerManagementService.Repository.ProfilesRepository;
using CustomerManagementService.Services.AccountServices;
using CustomerManagementService.Services.ProfileServices;
using Profile = AutoMapper.Profile;

namespace CustomerManagementService.Services.CustomerServices;

public class CustomerService : ICustomerService
{
    private readonly IMapper _mapper;

    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerAccountService _customerAccountService;
    private readonly ICustomerProfileService _customerProfileService;

    public CustomerService(IMapper mapper, ICustomerRepository customerRepository,
        ICustomerAccountService customerAccountService,
        ICustomerProfileService customerProfileService)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
        _customerAccountService = customerAccountService;
        _customerProfileService = customerProfileService;
    }

    public async Task<CustomerDto?> GetCustomerByIdAsync(string id)
    {
        var customer = await _customerRepository.GetCustomerByAuthIdAsync(id);
        if (customer == null)
        {
            throw new KeyNotFoundException("Customer not found.");
        }

        var accountDto = await _customerAccountService.GetAccountByCustomerIdAsync(customer.Id);
        if (accountDto == null)
        {
            throw new KeyNotFoundException("Account not found.");
        }

        var profileDto = await _customerProfileService.GetProfileByCustomerIdAsync(customer.Id);
        if (profileDto == null)
        {
            throw new KeyNotFoundException("Profile not found.");
        }

        var customerDto = _mapper.Map<CustomerDto>(customer);
        customerDto.CustomerAccountDto = accountDto;
        customerDto.CustomerProfileDto = profileDto;
        return customerDto;
    }

    public async Task<Guid?> GetCustomerIdByAuthIdAsync(string id)
    {
        var customerId = await _customerRepository.GetCustomerIdByAuthIdAsync(id);
        if (customerId == null)
        {
            throw new KeyNotFoundException("Customer not found.");
        }

        return customerId;
    }

    public async Task<Guid?> CreateCustomerAsync(CustomerDto customerDto)
    {
        string authId = customerDto.AuthId;
        var customerId = await _customerRepository.AddCustomerAsync(authId);
        
        await _customerAccountService.CreateAccountAsync(customerDto.CustomerAccountDto, customerId);
        await _customerProfileService.CreateProfileAsync(customerDto.CustomerProfileDto, customerId);
        return customerId;
    }

    public async Task DeleteCustomerAsync(string id)
    {
        var customerId = await _customerRepository.GetCustomerIdByAuthIdAsync(id);
        if (customerId == null)
        {
            throw new KeyNotFoundException("Customer not found.");
        }

        await _customerAccountService.DeleteAccountAsync(customerId.Value);
        await _customerProfileService.DeleteProfileAsync(customerId.Value);
        await _customerRepository.DeleteCustomerAsync(id);
    }
}