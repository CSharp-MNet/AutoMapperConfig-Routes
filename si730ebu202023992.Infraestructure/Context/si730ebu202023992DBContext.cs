using Microsoft.EntityFrameworkCore;
using si730ebu202023992.Infraestructure.Inventory.Model;
using si730ebu202023992.Infraestructure.Monitoring.Model;

namespace si730ebu202023992.Infraestructure.Context;

public class si730ebu202023992DBContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Snapshot> Snapshots { get; set; }
    
    public si730ebu202023992DBContext() { }
    
    public si730ebu202023992DBContext(DbContextOptions<si730ebu202023992DBContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql("Server=localhost,3306;Uid=root;Pwd=war280101;Database=db_war", serverVersion);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Product>().ToTable("Products");
        builder.Entity<Product>().HasKey(p => p.Id);
        builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Product>().Property(p => p.Brand).IsRequired().HasMaxLength(60);
        builder.Entity<Product>().Property(p => p.Model).IsRequired().HasMaxLength(60);
        builder.Entity<Product>().Property(p => p.SerialNumber).IsRequired().HasMaxLength(60);
        builder.Entity<Product>().Property(p => p.MonitoringLevel).IsRequired();
        
        builder.Entity<Snapshot>().ToTable("Snapshots");
        builder.Entity<Snapshot>().HasKey(s => s.Id);
        builder.Entity<Snapshot>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Snapshot>().Property(s => s.SnapshotId).IsRequired().HasMaxLength(60);
        builder.Entity<Snapshot>().Property(s => s.ProductSerialNumber).IsRequired().HasMaxLength(60);
        builder.Entity<Snapshot>().Property(s => s.Temperature).IsRequired();
        builder.Entity<Snapshot>().Property(s => s.Energy);
        builder.Entity<Snapshot>().Property(s => s.Leakage);
    }
}