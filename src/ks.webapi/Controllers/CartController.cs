using Microsoft.AspNetCore.Mvc;
using ks.application.Models.Cart;
using ks.application.Services.Interfaces;
using ks.domain.Entities;

namespace ks.webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ISessionCartService _sessionCartService;

        public CartController(ISessionCartService sessionCartService)
        {
            _sessionCartService = sessionCartService;
        }

        [HttpPost("add-fish")]
        public IActionResult AddFishToCart([FromBody] Fish fish)
        {
            _sessionCartService.AddFishToCart(fish);
            return Ok("Fish added to cart.");
        }

        [HttpPost("add-fish-package")]
        public IActionResult AddFishPackageToCart([FromBody] FishPackage fishPackage)
        {
            _sessionCartService.AddFishPackageToCart(fishPackage);
            return Ok("Fish package added to cart.");
        }

        [HttpGet]
        public IActionResult GetCart()
        {
            var cart = _sessionCartService.GetCart();
            return Ok(cart);
        }

        [HttpDelete]
        public IActionResult ClearCart()
        {
            _sessionCartService.ClearCart();
            return Ok("Cart cleared.");
        }
    }
}
