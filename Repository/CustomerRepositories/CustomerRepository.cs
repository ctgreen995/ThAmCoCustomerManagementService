using CustomerManagementService.Data.Models;
using CustomerManagementService.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementService.Repository.CustomerRepositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly DbContext _context;

    public CustomerRepository(DbContext context)
    {
        _context = context;
    }

    
    public async Task<Customer?> GetCustomerByAuthIdAsync(string id)
    {
        return await _context.Set<Customer>().FindAsync(id);
    }
    
    public async Task<Guid> AddCustomerAsync(string authId)
    {
        var customer = new Customer {AuthId = authId};
        var entityEntry = await _context.Set<Customer>().AddAsync(customer);
        await _context.SaveChangesAsync();
        return entityEntry.Entity.Id;
    }
    
    public async Task<Profile> UpdateCustomerAsync(Profile profile)
    {
        _context.Set<Profile>().Update(profile);
        await _context.SaveChangesAsync();
        return profile;
    }
    
    public async Task DeleteCustomerAsync(string id)
    {
        var customer = await _context.Set<Profile>().FindAsync(id);
        if (customer != null) _context.Set<Profile>().Remove(customer);
        await _context.SaveChangesAsync();
    }
}