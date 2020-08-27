using eMarketApp.Helpers;
using eMarketApp.Repositories;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace eMarketApp.Pages.Carrito
{
    [Authorize]
    public class ViewModel : PageModel
    {
        private readonly ICartRepository _cartRepository;

        public Cart carrito { get; set; }

        public int ProductsAmount { get; set; }

        public ViewModel(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public ActionResult OnGet()
        {
            ProductsAmount = 0;
            carrito = SessionHelper.GetObject<Cart>(HttpContext.Session, "CART");
            if (carrito != null && carrito.Products != null)
                ProductsAmount = carrito.Products.Count;
            return Page();
        }

        public async Task<ActionResult> OnPostCheckout()
        {
            var cart = SessionHelper.GetObject<Cart>(HttpContext.Session, "CART");
            cart.Username = User.Identity.Name;
            var result = await _cartRepository.Checkout(cart);
            if (result)
            {
                ViewData.Add("Success", true);
                SessionHelper.SetObject(HttpContext.Session, "CART", null);
            }
            else
            {
                ViewData.Add("Success", false);
            }
            return OnGet();
        }
    }
}