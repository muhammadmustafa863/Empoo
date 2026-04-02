using MartPOS.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace MartPOS.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleItem> SaleItems => Set<SaleItem>();
    public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
    public DbSet<PurchaseOrderItem> PurchaseOrderItems => Set<PurchaseOrderItem>();
    public DbSet<StockAdjustment> StockAdjustments => Set<StockAdjustment>();
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<CashDrawer> CashDrawers => Set<CashDrawer>();
    public DbSet<LoyaltyTransaction> LoyaltyTransactions => Set<LoyaltyTransaction>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<AppSetting> AppSettings => Set<AppSetting>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(x => x.Username).IsUnique();
        modelBuilder.Entity<Product>().HasIndex(x => x.Barcode).IsUnique();
        modelBuilder.Entity<Sale>().HasIndex(x => x.InvoiceNumber).IsUnique();
        modelBuilder.Entity<Category>().HasOne(x => x.ParentCategory).WithMany().HasForeignKey(x => x.ParentCategoryId).OnDelete(DeleteBehavior.Restrict);
    }
}
