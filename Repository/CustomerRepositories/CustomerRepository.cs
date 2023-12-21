using CustomerManagementService.Data;
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
        if (customer != null)
        {
            return customer.Id;
        }

        return null;
    }

    public async Task<Guid?> AddCustomerAsync(string authId)
    {
        var customer = new Customer { AuthId = authId };
        var entityEntry = await _context.Set<Customer>().AddAsync(customer);
        await _context.SaveChangesAsync();
        return entityEntry.Entity.Id;
    }

    public async Task DeleteCustomerAsync(Guid? id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}