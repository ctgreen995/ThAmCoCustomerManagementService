using AutoMapper;
using CustomerManagementService.Data;
using CustomerManagementService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementService.Repository.ProfilesRepository;

public class CustomerProfileRepository : ICustomerProfileRepository
{
    private readonly CustomerDbContext _context;
    
    public CustomerProfileRepository(CustomerDbContext context)
    {
        _context = context;
    }   
    public async Task AddProfileAsync(CustomerProfile profile)
    {
        await _context.Profiles.AddAsync(profile);
        await _context.SaveChangesAsync();
    }

    public async Task<CustomerProfile?> GetProfileByCustomerIdAsync(Guid customerId)
    {
        return await _context.Profiles.FirstOrDefaultAsync(p => p.CustomerId == customerId);
    }

    public async Task UpdateProfileByCustomerIdAsync(CustomerProfile profile)
    {
        var profileToUpdate = _context.Profiles.FirstOrDefault(p => p.CustomerId == profile.CustomerId);
        if (profileToUpdate == null)
        {
            throw new KeyNotFoundException("Profile not found.");
        }
        _context.Update(profile);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProfileAsync(Guid customerId)
    {
        // Remove profile from database as this is the table that contains PII.
        // This can be adjusted if some PII is to be kept for future use.
        var profileToDelete = await _context.Profiles.FirstOrDefaultAsync(p => p.CustomerId == customerId);
        if (profileToDelete == null)
        {
            throw new KeyNotFoundException("Profile not found.");
        }
        _context.Profiles.Remove(profileToDelete);
        await _context.SaveChangesAsync();
    }
}