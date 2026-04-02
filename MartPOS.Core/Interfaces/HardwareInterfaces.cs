using MartPOS.Core.Models;

namespace MartPOS.Core.Interfaces;

public interface IBarcodeScanner
{
    Task<string> ScanAsync();
}

public interface IThermalPrinter
{
    bool IsConnected { get; }
    Task PrintReceiptAsync(Sale sale);
    Task PrintBarcodeLabel(Product product);
    Task OpenCashDrawer();
}

public record PaymentResult(bool IsSuccess, string TransactionId, string? Message = null);

public interface IPaymentTerminal
{
    Task<PaymentResult> ProcessPaymentAsync(decimal amount);
}
