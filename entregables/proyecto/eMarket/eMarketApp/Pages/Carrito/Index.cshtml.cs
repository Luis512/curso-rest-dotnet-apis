using eMarketApp.Helpers;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eMarketApp.Pages.Carrito
{
    [Authorize]
    public class ViewModel : PageModel
    {
        public Cart carrito { get; set; }

        public int ProductsAmount { get; set; }
        public ActionResult OnGet()
        {
            ProductsAmount = 0;
            carrito = SessionHelper.GetObject<Cart>(HttpContext.Session, "CART");
            if (carrito != null && carrito.Products != null)
                ProductsAmount = carrito.Products.Count;
            return Page();
        }
    }
}