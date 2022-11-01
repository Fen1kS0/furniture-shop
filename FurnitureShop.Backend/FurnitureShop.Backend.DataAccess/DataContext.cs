using System.Reflection;
using FurnitureShop.Backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FurnitureShop.Backend.DataAccess;

public class DataContext : DbContext
{
    private readonly IConfiguration _config;

    public DataContext()
    {
        _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Buyer> Buyers { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Furniture> Furniture { get; set; }
    public DbSet<Sale> Sales { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}