using MartPOS.Common;

namespace MartPOS.Core.Models;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public bool IsActive { get; set; } = true;
    public bool MustChangePassword { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public int? SupplierId { get; set; }
    public decimal CostPrice { get; set; }
    public decimal SalePrice { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal TaxPercent { get; set; }
    public decimal StockQuantity { get; set; }
    public decimal MinStockLevel { get; set; }
    public string Unit { get; set; } = "pcs";
    public string? ImagePath { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public Category? Category { get; set; }
    public Supplier? Supplier { get; set; }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
}

public class Supplier
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string? ContactPerson { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public int LoyaltyPoints { get; set; }
    public decimal TotalPurchases { get; set; }
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}

public class Sale
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public int? CustomerId { get; set; }
    public int UserId { get; set; }
    public decimal SubTotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal GrandTotal { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal Change { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public SaleStatus Status { get; set; } = SaleStatus.Completed;
    public string? Notes { get; set; }
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;

    public List<SaleItem> Items { get; set; } = new();
}

public class SaleItem
{
    public int Id { get; set; }
    public int SaleId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal TaxPercent { get; set; }
    public decimal LineTotal { get; set; }
}

public class PurchaseOrder { public int Id { get; set; } public string PONumber { get; set; } = string.Empty; public int SupplierId { get; set; } public int UserId { get; set; } public PurchaseOrderStatus Status { get; set; } public decimal TotalAmount { get; set; } public string? Notes { get; set; } public DateTime OrderDate { get; set; } = DateTime.UtcNow; public DateTime? ReceivedDate { get; set; } }
public class PurchaseOrderItem { public int Id { get; set; } public int PurchaseOrderId { get; set; } public int ProductId { get; set; } public decimal Quantity { get; set; } public decimal ReceivedQuantity { get; set; } public decimal UnitCost { get; set; } public decimal LineTotal { get; set; } }
public class StockAdjustment { public int Id { get; set; } public int ProductId { get; set; } public int UserId { get; set; } public StockAdjustmentType AdjustmentType { get; set; } public decimal PreviousQty { get; set; } public decimal AdjustedQty { get; set; } public decimal NewQty { get; set; } public string Reason { get; set; } = string.Empty; public DateTime AdjustedAt { get; set; } = DateTime.UtcNow; }
public class Expense { public int Id { get; set; } public string Title { get; set; } = string.Empty; public string Category { get; set; } = string.Empty; public decimal Amount { get; set; } public PaymentMethod PaymentMethod { get; set; } public int UserId { get; set; } public string? Notes { get; set; } public DateTime ExpenseDate { get; set; } = DateTime.UtcNow; }
public class CashDrawer { public int Id { get; set; } public int UserId { get; set; } public decimal OpeningBalance { get; set; } public decimal ClosingBalance { get; set; } public decimal TotalSales { get; set; } public decimal TotalRefunds { get; set; } public decimal TotalExpenses { get; set; } public DateTime OpenedAt { get; set; } = DateTime.UtcNow; public DateTime? ClosedAt { get; set; } public CashDrawerStatus Status { get; set; } = CashDrawerStatus.Open; }
public class LoyaltyTransaction { public int Id { get; set; } public int CustomerId { get; set; } public int? SaleId { get; set; } public int PointsEarned { get; set; } public int PointsRedeemed { get; set; } public int Balance { get; set; } public DateTime TransactionDate { get; set; } = DateTime.UtcNow; }
public class AuditLog { public int Id { get; set; } public int? UserId { get; set; } public string Action { get; set; } = string.Empty; public string TableName { get; set; } = string.Empty; public string RecordId { get; set; } = string.Empty; public string? OldValues { get; set; } public string? NewValues { get; set; } public DateTime Timestamp { get; set; } = DateTime.UtcNow; public string? IPAddress { get; set; } }
public class AppSetting { public int Id { get; set; } public string Key { get; set; } = string.Empty; public string Value { get; set; } = string.Empty; public string? Description { get; set; } }
