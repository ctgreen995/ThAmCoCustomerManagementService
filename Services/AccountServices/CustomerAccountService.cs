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

    public CustomerAccountService(IMapper mapper, ICustomerAccountRepository customerAccountRepository)
    {
        _mapper = mapper;
        _customerAccountRepository = customerAccountRepository;
    }

    public async Task<CustomerAccountDto> GetAccountByCustomerIdAsync(Guid? customerId)
    {
        if (customerId == null)
        {
            throw new ArgumentNullException(nameof(customerId));
        }
        var account = await _customerAccountRepository.GetAccountByCustomerIdAsync(customerId);
        if (account == null)
        {
            throw new KeyNotFoundException("Account not found.");
        }
        var accountDto = _mapper.Map<CustomerAccountDto>(account);
        return accountDto;
    }

    public async Task CreateAccountAsync(CustomerAccountDto customerAccountDto, Guid? customerId)
    {
        if(customerId == null) throw new ArgumentNullException(nameof(customerId));
        var account = _mapper.Map<CustomerAccount>(customerAccountDto);
        account.CustomerId = customerId;
        await _customerAccountRepository.AddAccountAsync(account);
    }

    public async Task UpdateAccountByCustomerIdAsync(Guid? customerId, CustomerAccountDto customerAccountDto)
    {
        if(customerId == null) throw new ArgumentNullException(nameof(customerId));
        var account = _mapper.Map<CustomerAccount>(customerAccountDto);

        account.CustomerId = customerId;
        await _customerAccountRepository.UpdateAccountByCustomerIdAsync(account);
    }
}