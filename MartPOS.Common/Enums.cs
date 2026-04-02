namespace MartPOS.Common;

public enum UserRole { Admin = 1, Manager = 2, Cashier = 3 }
public enum PaymentMethod { Cash = 1, Card = 2, Mobile = 3 }
public enum SaleStatus { Completed = 1, Refunded = 2, Voided = 3 }
public enum PurchaseOrderStatus { Draft = 1, Ordered = 2, Received = 3, Cancelled = 4 }
public enum StockAdjustmentType { Addition = 1, Deduction = 2, Correction = 3 }
public enum CashDrawerStatus { Open = 1, Closed = 2 }
