using MartPOS.Core.Models;

namespace MartPOS.Core.Interfaces;

public interface ISalesService
{
    Task<Sale> CreateSaleAsync(Sale sale, CancellationToken ct = default);
    string GenerateInvoiceNumber(DateTime nowUtc);
    decimal CalculateLineTotal(decimal qty, decimal unitPrice, decimal discountPct, decimal taxPct);
}
