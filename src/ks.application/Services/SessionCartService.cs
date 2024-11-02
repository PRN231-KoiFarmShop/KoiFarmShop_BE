using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ks.domain.Entities;
using ks.application.Models.Cart;
using ks.application.Services.Interfaces;

namespace ks.application.Services
{
    public class SessionCartService : ISessionCartService
    {
        private readonly ISession _session;
        private const string CartSessionKey = "UserCart";

        public SessionCartService(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public CartViewModel GetCart()
        {
            var cartJson = _session.GetString(CartSessionKey);
            var cart = string.IsNullOrEmpty(cartJson) ? new Cart() : JsonConvert.DeserializeObject<Cart>(cartJson);

            return new CartViewModel
            {
                UserId = cart.UserId,
                Items = cart.Fishes.Select(f => new CartItemModel
                {
                    ProductId = f.Id,
                    ProductType = "Fish",
                    Name = f.Name,
                    Price = f.Price,
                    Quantity = 1 // Assuming each fish is a single item
                }).Concat(cart.FishPackages.Select(fp => new CartItemModel
                {
                    ProductId = fp.Id,
                    ProductType = "FishPackage",
                    Name = fp.Description,
                    Price = fp.TotalPrice,
                    Quantity = 1 // Assuming each package is a single item
                })).ToList()
            };
        }

        public void AddFishToCart(Fish fish)
        {
            var cart = GetOrCreateCart();
            cart.Fishes.Add(fish);
            SaveCart(cart);
        }

        public void AddFishPackageToCart(FishPackage fishPackage)
        {
            var cart = GetOrCreateCart();
            cart.FishPackages.Add(fishPackage);
            SaveCart(cart);
        }

        public void ClearCart()
        {
            _session.Remove(CartSessionKey);
        }

        private Cart GetOrCreateCart()
        {
            var cartJson = _session.GetString(CartSessionKey);
            return string.IsNullOrEmpty(cartJson) ? new Cart() : JsonConvert.DeserializeObject<Cart>(cartJson);
        }

        private void SaveCart(Cart cart)
        {
            var cartJson = JsonConvert.SerializeObject(cart);
            _session.SetString(CartSessionKey, cartJson);
        }
    }
}
