using AutoMapper;
using CustomerManagementService.Data.Models;
using CustomerManagementService.Dtos;
using CustomerManagementService.Models;
using CustomerManagementService.Repository.AccountRepositories;
using CustomerManagementService.Repository.CustomerRepositories;
using CustomerManagementService.Repository.ProfilesRepository;
using Profile = AutoMapper.Profile;

namespace CustomerManagementService.Services.CustomerServices;

public class CustomerService : ICustomerService
{
    private readonly IMapper _mapper;

    private readonly ICustomerRepository _customerRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IProfileRepository _profileRepository;

    public CustomerService(IMapper mapper, ICustomerRepository customerRepository, IAccountRepository accountRepository,
        IProfileRepository profileRepository)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
        _accountRepository = accountRepository;
        _profileRepository = profileRepository;
    }

    public async Task<CustomerDto?> GetCustomerByIdAsync(string id)
    {
        var customer = await _customerRepository.GetCustomerByAuthIdAsync(id);
        if (customer == null)
        {
            return null;
        }
        var account = await _accountRepository.GetAccountByCustomerIdAsync(customer.Id);
        var profile = await _profileRepository.GetProfileByCustomerIdAsync(customer.Id);
        
        var customerDto = _mapper.Map<CustomerDto>(customer);
        customerDto.Account = _mapper.Map<AccountDto>(account);
        customerDto.Profile = _mapper.Map<ProfileDto>(profile);
        return customerDto;
    }

    public async Task<Guid?> CreateCustomerAsync(CustomerDto customerDto)
    {
        string authId = customerDto.AuthId;
        var account = _mapper.Map<Account>(customerDto.Account);
        var profile = _mapper.Map<Profile>(customerDto.Profile);
        Guid customerId = await _customerRepository.AddCustomerAsync(authId);
        await _accountRepository.AddAccountAsync(account, customerId);
        await _profileRepository.AddProfileAsync(profile, customerId);
        return customerId;
    }

    public Task DeleteCustomerAsync(string id)
    {
        throw new NotImplementedException();
    }
}