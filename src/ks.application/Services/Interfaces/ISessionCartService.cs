using ks.application.Models.Cart;
using ks.domain.Entities;

namespace ks.application.Services.Interfaces
{
    public interface ISessionCartService
    {
        CartViewModel GetCart();
        void AddFishToCart(Fish fish);
        void AddFishPackageToCart(FishPackage fishPackage);
        void ClearCart();
    }
}
