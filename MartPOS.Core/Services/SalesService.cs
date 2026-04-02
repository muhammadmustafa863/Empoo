using MartPOS.Core.Interfaces;
using MartPOS.Core.Models;

namespace MartPOS.Core.Services;

public class SalesService : ISalesService
{
    private static int _dailySequence = 1;
    private static DateOnly _sequenceDate = DateOnly.FromDateTime(DateTime.UtcNow);

    public Task<Sale> CreateSaleAsync(Sale sale, CancellationToken ct = default)
    {
        sale.SubTotal = sale.Items.Sum(i => i.Quantity * i.UnitPrice);
        sale.TaxAmount = sale.Items.Sum(i => (i.Quantity * i.UnitPrice - ((i.DiscountPercent / 100m) * i.Quantity * i.UnitPrice)) * (i.TaxPercent / 100m));
        sale.GrandTotal = sale.SubTotal - sale.DiscountAmount + sale.TaxAmount;
        sale.Change = Math.Max(0, sale.AmountPaid - sale.GrandTotal);
        if (string.IsNullOrWhiteSpace(sale.InvoiceNumber))
            sale.InvoiceNumber = GenerateInvoiceNumber(DateTime.UtcNow);
        return Task.FromResult(sale);
    }

    public string GenerateInvoiceNumber(DateTime nowUtc)
    {
        var today = DateOnly.FromDateTime(nowUtc);
        if (today != _sequenceDate)
        {
            _sequenceDate = today;
            _dailySequence = 1;
        }

        return $"INV-{nowUtc:yyyyMMdd}-{_dailySequence++:0000}";
    }

    public decimal CalculateLineTotal(decimal qty, decimal unitPrice, decimal discountPct, decimal taxPct)
    {
        var baseAmount = qty * unitPrice;
        var discounted = baseAmount - (baseAmount * discountPct / 100m);
        return discounted + (discounted * taxPct / 100m);
    }
}
