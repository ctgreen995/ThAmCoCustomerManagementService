using CustomerDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerDatabase.Data;

public class CustomerDbContext : DbContext
{
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Session> Sessions { get; set; }

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
            entity.ToTable("Customers");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Town).HasMaxLength(50);
            entity.Property(e => e.County).HasMaxLength(50);
            entity.Property(e => e.Postcode).HasMaxLength(10);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(50);
        });
        
        modelBuilder.Entity<Session>(entity =>
        {
            entity.ToTable("Sessions");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasMaxLength(10);
            entity.Property(e => e.CustomerId).HasMaxLength(10);
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Length).HasMaxLength(10);
            entity.HasOne(d => d.Customer)
                .WithMany(p => p.Sessions)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sessions_Customers");
        });
        
        // Seed data for Customers
        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = "Cust01", Name = "John Doe", Address = "123 Main St", Town = "Springfield", County = "Shelby", Postcode = "12345", Phone = "555-1234", Email = "johndoe@example.com" },
            new Customer { Id = "Cust02", Name = "Jane Smith", Address = "456 Elm St", Town = "Greenville", County = "Franklin", Postcode = "23456", Phone = "555-5678", Email = "janesmith@example.com" }
        );

        // Seed data for Sessions
        modelBuilder.Entity<Session>().HasData(
            new Session { Id = "Sess01", CustomerId = "Cust01", Date = new DateTime(2023, 12, 1), Length = "1 hour" },
            new Session { Id = "Sess02", CustomerId = "Cust01", Date = new DateTime(2023, 12, 5), Length = "2 hours" },
            new Session { Id = "Sess03", CustomerId = "Cust02", Date = new DateTime(2023, 12, 3), Length = "1.5 hours" }
        );
    }
}
