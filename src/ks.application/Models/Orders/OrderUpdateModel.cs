using ks.domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ks.application.Models.Orders
{
    public class OrderUpdateModel
    {
        public int TotalQuantity { get; set; }
        public double TotalAmount { get; set; }
        public double ActualAmount { get; set; }
        public double SalePercent { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public PaymentMethodEnum PaymentMethod { get; set; }
    }
}
