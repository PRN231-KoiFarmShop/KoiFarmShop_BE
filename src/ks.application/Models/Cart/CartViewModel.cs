using System;
using System.Collections.Generic;
using System.Linq;

namespace ks.application.Models.Cart
{
    public class CartViewModel
    {
        public Guid UserId { get; set; }
        public List<CartItemModel> Items { get; set; } = new List<CartItemModel>();
        public double TotalPrice => CalculateTotalPrice();

        private double CalculateTotalPrice()
        {
            return Items.Sum(item => item.Price * item.Quantity);
        }
    }
} 