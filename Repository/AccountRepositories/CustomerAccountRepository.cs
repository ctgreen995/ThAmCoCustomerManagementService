using CustomerManagementService.Data;
using CustomerManagementService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementService.Repository.AccountRepositories;

public class CustomerAccountRepository : ICustomerAccountRepository
{
    private readonly CustomerDbContext _context;
    
    public CustomerAccountRepository(CustomerDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAccountAsync(CustomerAccount customerAccount)
    {
        _context.Accounts.Add(customerAccount);
        await _context.SaveChangesAsync();
    }

    public Task<CustomerAccount?> GetAccountByCustomerIdAsync(Guid customerId)
    {
        return _context.Accounts.FirstOrDefaultAsync(a => a.CustomerId == customerId);
    }
    
    public async Task UpdateAccountByCustomerIdAsync(CustomerAccount customerAccount)
    {
        var accountToUpdate = await _context.Accounts.FirstOrDefaultAsync(a => a.CustomerId == customerAccount.CustomerId);
        if (accountToUpdate == null)
        {
            throw new KeyNotFoundException("Account not found.");
        }
        _context.Update(customerAccount);
        await _context.SaveChangesAsync();
    }

    public Task DeleteAccountAsync(Guid customerId)
    {
        // No PII contained in customer account table, so no need to delete.
        // This remains here for future use.
        return Task.CompletedTask;
    }
}