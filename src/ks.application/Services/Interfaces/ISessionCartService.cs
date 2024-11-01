using ks.domain.Entities;

namespace ks.application.Services.Interfaces
{
    public interface ISessionCartService
    {
        Cart GetCart();
        void AddFishToCart(Fish fish);
        void AddFishPackageToCart(FishPackage fishPackage);
        void ClearCart();
    }
}
