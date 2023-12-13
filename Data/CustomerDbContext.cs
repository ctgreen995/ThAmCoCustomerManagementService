using CustomerManagementService.Data.Models;
using CustomerManagementService.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementService.Data;

public class CustomerDbContext : DbContext
{
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Profile> Profiles { get; set; }
    public virtual DbSet<Account> Accounts { get; set; }

    public CustomerDbContext()
    {
    }

    public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:CustomerDbConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AuthId).HasMaxLength(50);
            entity.Property(e => e.Id).HasMaxLength(50);
        });
        modelBuilder.Entity<Profile>(entity =>
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
                .WithOne(p => p.Profile)
                .HasForeignKey<Profile>(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Profiles_Customers");
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CustomerId).HasMaxLength(50);
            entity.Property(e => e.Funds).HasColumnType("decimal(18, 2)");
            entity.HasOne(d => d.Customer)
                .WithOne(p => p.Account)
                .HasForeignKey<Account>(d => d.CustomerId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Accounts_Customers");
        });
        
        Guid customer1Id = Guid.NewGuid();
        Guid customer2Id = Guid.NewGuid();

        // Seed data for Customers
        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = customer1Id },
            new Customer { Id = customer2Id }
        );

        // Seed data for Profiles
        modelBuilder.Entity<Profile>().HasData(
            new Profile
            {
                Id = new Guid(), CustomerId = customer1Id, Name = "John Smith", Address = "1 High Street", Town = "Anytown",
                County = "Anyshire", Postcode = "AB1 2CD", Phone = "01234 567890", Email = "jsmith@gmail.com"
            },
            new Profile
            {
                Id = new Guid(), CustomerId = customer2Id, Name = "Jane Doe", Address = "2 High Street", Town = "Anytown",
                County = "Anyshire", Postcode = "AB1 2CD", Phone = "01234 567890", Email = "jdoe~gmail.com"
            }
        );

        // Seed data for Accounts
        modelBuilder.Entity<Account>().HasData(
            new Account { Id = new Guid(), CustomerId = customer1Id, Funds = 100.00 },
            new Account { Id = new Guid(), CustomerId = customer2Id, Funds = 200.00 }
        );
    }
}