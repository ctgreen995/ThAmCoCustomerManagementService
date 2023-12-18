using CustomerManagementService.Data;
using CustomerManagementService.Data.Models;
using CustomerManagementService.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementService.Repository.CustomerRepositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerDbContext _context;

    public CustomerRepository(CustomerDbContext context)
    {
        _context = context;
    }

    
    public async Task<Customer?> GetCustomerByAuthIdAsync(string id)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.AuthId == id);
    }

    public async Task<Guid?> GetCustomerIdByAuthIdAsync(string id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.AuthId == id);
        return customer?.Id;
    }

    public async Task<Guid> AddCustomerAsync(string authId)
    {
        var customer = new Customer {AuthId = authId};
        var entityEntry = await _context.Set<Customer>().AddAsync(customer);
        await _context.SaveChangesAsync();
        return entityEntry.Entity.Id;
    }
    
    public async Task<CustomerProfile> UpdateCustomerAsync(CustomerProfile customerProfile)
    {
        _context.Set<CustomerProfile>().Update(customerProfile);
        await _context.SaveChangesAsync();
        return customerProfile;
    }
    
    public Task DeleteCustomerAsync(string id)
    {
        // No PII contained in customer table, so no need to delete.
        // This remains here for future use.
        return Task.CompletedTask;
    }
}