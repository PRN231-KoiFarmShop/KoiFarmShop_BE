using ks.domain.Enums;

namespace ks.application.Models.Orders;
public class OrderCreateModel 
{
    public double SalePercent { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
    public PaymentMethodEnum PaymentMethod { get; set; }
    public List<Guid> Items { get; set; }
}