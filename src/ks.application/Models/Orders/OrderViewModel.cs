using ks.application.Models.OrderLines;
using ks.domain.Enums;

namespace ks.application.Models.Orders
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalAmount { get; set; }
        public double ActualAmount { get; set; }
        public double SalePercent { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public PaymentMethodEnum PaymentMethod { get; set; }
    }
}
