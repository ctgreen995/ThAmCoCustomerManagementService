using AutoMapper;
using CustomerManagementService.Dtos;
using CustomerManagementService.Repository.CustomerRepositories;
using CustomerManagementService.Services.AccountServices;
using CustomerManagementService.Services.ProfileServices;

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
        var formattedId = id.Substring(6);
        var customer = await _customerRepository.GetCustomerByAuthIdAsync(formattedId);
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
        return customerId;
    }

    public async Task<Guid?> CreateCustomerAsync(CustomerDto customerDto)
    {
        if (customerDto == null || customerDto.CustomerAccountDto == null || customerDto.CustomerProfileDto == null)
        {
            throw new ArgumentNullException(nameof(customerDto));
        }

        string authId = customerDto.AuthId;
        var customerId = await _customerRepository.AddCustomerAsync(authId);
        await _customerAccountService.CreateAccountAsync(customerDto.CustomerAccountDto, customerId);
        await _customerProfileService.CreateProfileAsync(customerDto.CustomerProfileDto, customerId);
        return customerId;
    }

    public async Task DeleteCustomerAsync(string id)
    {
        var customerId = await _customerRepository.GetCustomerIdByAuthIdAsync(id);

        await _customerRepository.DeleteCustomerAsync(customerId);
    }

    public async Task UpdateCustomerAsync(CustomerDto customerDto)
    {
        if (customerDto == null || customerDto.CustomerAccountDto == null || customerDto.CustomerProfileDto == null)
        {
            throw new ArgumentNullException(nameof(customerDto));
        }

        var customerId = await _customerRepository.GetCustomerIdByAuthIdAsync(customerDto.AuthId);
        await _customerAccountService.UpdateAccountByCustomerIdAsync(customerId, customerDto.CustomerAccountDto);
        await _customerProfileService.UpdateProfileByCustomerIdAsync(customerId, customerDto.CustomerProfileDto);
    }
}