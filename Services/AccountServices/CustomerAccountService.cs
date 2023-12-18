using AutoMapper;
using CustomerManagementService.Data.Models;
using CustomerManagementService.Dtos;
using CustomerManagementService.Repository.AccountRepositories;
using CustomerManagementService.Services.CustomerServices;

namespace CustomerManagementService.Services.AccountServices;

public class CustomerAccountService : ICustomerAccountService
{
    private readonly IMapper _mapper;
    private readonly ICustomerAccountRepository _customerAccountRepository;
    private readonly ICustomerService _customerService;

    public CustomerAccountService(IMapper mapper, ICustomerAccountRepository customerAccountRepository,
        ICustomerService customerService)
    {
        _mapper = mapper;
        _customerAccountRepository = customerAccountRepository;
        _customerService = customerService;
    }

    public async Task<CustomerAccountDto> GetAccountByCustomerIdAsync(Guid customerId)
    {
        var account = await _customerAccountRepository.GetAccountByCustomerIdAsync(customerId);
        var accountDto = _mapper.Map<CustomerAccountDto>(account);
        return accountDto;
    }

    public async Task CreateAccountAsync(CustomerAccountDto customerAccountDto, Guid customerId)
    {
        var account = _mapper.Map<CustomerAccount>(customerAccountDto);
        account.CustomerId = customerId;
        await _customerAccountRepository.AddAccountAsync(account);
    }

    public async Task DeleteAccountAsync(Guid customerId)
    {
        await _customerAccountRepository.DeleteAccountAsync(customerId);
    }

    public async Task UpdateAccountByAuthIdAsync(string authId, CustomerAccountDto customerAccountDto)
    {
        var account = _mapper.Map<CustomerAccount>(customerAccountDto);
        var customerId = await _customerService.GetCustomerIdByAuthIdAsync(authId);
        if (customerId != null)
        {
            account.CustomerId = customerId.Value;
            await _customerAccountRepository.UpdateAccountByCustomerIdAsync(account);
        }
        else throw new KeyNotFoundException("Customer not found.");
    }
}