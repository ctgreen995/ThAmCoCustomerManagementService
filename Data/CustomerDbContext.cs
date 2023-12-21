using CustomerManagementService.Data.Models;
using CustomerManagementService.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementService.Data;

public class CustomerDbContext : DbContext
{
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<CustomerProfile> Profiles { get; set; }
    public virtual DbSet<CustomerAccount> Accounts { get; set; }

    public CustomerDbContext()
    {
    }

    public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AuthId).HasMaxLength(50);
            entity.Property(e => e.Id).HasMaxLength(50);
        });
        modelBuilder.Entity<CustomerProfile>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CustomerId).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Town).HasMaxLength(50);
            entity.Property(e => e.County).HasMaxLength(50);
            entity.Property(e => e.Postcode).HasMaxLength(10);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.HasOne(d => d.Customer)
                .WithOne(p => p.CustomerProfile)
                .HasForeignKey<CustomerProfile>(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Profiles_Customers");
        });

        modelBuilder.Entity<CustomerAccount>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CustomerId).HasMaxLength(50);
            entity.Property(e => e.Funds).HasColumnType("decimal(18, 2)");
            entity.HasOne(d => d.Customer)
                .WithOne(p => p.CustomerAccount)
                .HasForeignKey<CustomerAccount>(d => d.CustomerId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Accounts_Customers");
        });
        
        Guid customer1Id = Guid.NewGuid();
        Guid customer2Id = Guid.NewGuid();

        // Seed data for Customers
        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = customer1Id, AuthId = "auth0|60a0a0a0a0a0a0a0a0a0a0a0"},
            new Customer { Id = customer2Id, AuthId = "auth0|60a0a0a0a0a0a0a0a0a0a0a1"}
        );

        // Seed data for Profiles
        modelBuilder.Entity<CustomerProfile>().HasData(
            new CustomerProfile
            {
                Id = Guid.NewGuid(), CustomerId = customer1Id, Name = "John Smith", Address = "1 High Street", Town = "Anytown",
                County = "Anyshire", Postcode = "AB1 2CD", Phone = "01234 567890", Email = "jsmith@gmail.com"
            },
            new CustomerProfile
            {
                Id = Guid.NewGuid(), CustomerId = customer2Id, Name = "Jane Doe", Address = "2 High Street", Town = "Anytown",
                County = "Anyshire", Postcode = "AB1 2CD", Phone = "01234 567890", Email = "jdoe~gmail.com"
            }
        );

        // Seed data for Accounts
        modelBuilder.Entity<CustomerAccount>().HasData(
            new CustomerAccount { Id = Guid.NewGuid(), CustomerId = customer1Id, Funds = 100.00 },
            new CustomerAccount { Id = Guid.NewGuid(), CustomerId = customer2Id, Funds = 200.00 }
        );
    }
}