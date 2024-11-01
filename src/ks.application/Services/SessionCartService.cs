using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ks.domain.Entities;
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

        public Cart GetCart()
        {
            var cartJson = _session.GetString(CartSessionKey);
            return string.IsNullOrEmpty(cartJson) ? new Cart() : JsonConvert.DeserializeObject<Cart>(cartJson);
        }

        public void AddFishToCart(Fish fish)
        {
            var cart = GetCart();
            cart.Fishes.Add(fish);
            SaveCart(cart);
        }

        public void AddFishPackageToCart(FishPackage fishPackage)
        {
            var cart = GetCart();
            cart.FishPackages.Add(fishPackage);
            SaveCart(cart);
        }

        public void ClearCart()
        {
            _session.Remove(CartSessionKey);
        }

        private void SaveCart(Cart cart)
        {
            var cartJson = JsonConvert.SerializeObject(cart);
            _session.SetString(CartSessionKey, cartJson);
        }
    }
}
