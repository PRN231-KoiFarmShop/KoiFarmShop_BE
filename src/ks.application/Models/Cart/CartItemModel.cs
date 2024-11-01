using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ks.application.Models.Cart
{
    public class CartItemModel
    {
        public Guid ProductId { get; set; }
        public string ProductType { get; set; } = string.Empty; // "Fish" or "FishPackage"
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
