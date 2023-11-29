using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data;

public class CustomerDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public CustomerDbContext()
    {
    }

    public CustomerDbContext(DbContextOptions<DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CostData> CostData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:SmartMerchantDbConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
