using MartPOS.Core.Interfaces;
using MartPOS.Core.Models;

namespace MartPOS.UI.Forms;

public class SalesForm : Form
{
    private readonly ISalesService _salesService;
    private readonly TextBox _barcodeBox = new() { PlaceholderText = "Scan barcode and press Enter" };
    private readonly DataGridView _cart = new() { Dock = DockStyle.Fill, AutoGenerateColumns = false };
    private readonly Label _totals = new() { Dock = DockStyle.Bottom, Height = 40 };
    private readonly List<SaleItem> _items = new();

    public SalesForm(ISalesService salesService)
    {
        _salesService = salesService;
        Text = "Mart POS - Sales";
        Width = 1200;
        Height = 760;

        _cart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "Item" });
        _cart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Qty" });
        _cart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UnitPrice", HeaderText = "Price" });
        _cart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "LineTotal", HeaderText = "Line Total" });

        _barcodeBox.Dock = DockStyle.Top;
        _barcodeBox.KeyDown += BarcodeBoxOnKeyDown;

        Controls.Add(_cart);
        Controls.Add(_totals);
        Controls.Add(_barcodeBox);

        BindCart();
    }

    private async void BarcodeBoxOnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter || string.IsNullOrWhiteSpace(_barcodeBox.Text)) return;

        var code = _barcodeBox.Text.Trim();
        var item = new SaleItem
        {
            ProductId = 0,
            Barcode = code,
            ProductName = $"Scanned-{code}",
            Quantity = 1,
            UnitPrice = 100,
            DiscountPercent = 0,
            TaxPercent = 17,
            LineTotal = _salesService.CalculateLineTotal(1, 100, 0, 17)
        };
        _items.Add(item);
        _barcodeBox.Clear();

        var sale = await _salesService.CreateSaleAsync(new Sale { Items = _items, AmountPaid = 0 });
        _totals.Text = $"Subtotal: {sale.SubTotal:N2} | Tax: {sale.TaxAmount:N2} | Total: {sale.GrandTotal:N2}";
        BindCart();
    }

    private void BindCart()
    {
        _cart.DataSource = null;
        _cart.DataSource = _items;
    }
}
