using System.Collections.Generic;
using System.Threading.Tasks;
using eMarketApp.Helpers;
using eMarketApp.Repositories;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eMarketApp.Pages.Categorias
{
    [Authorize]
    public class ViewModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public Category categoria { get; set; }

        public List<Product> products {get; set;}
        
        public ViewModel(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            categoria = new Category();
            categoria = await _categoryRepository.GetCategoryById(id);
            if (categoria != null)
            {
                products = await _productRepository.GetProductsByCategory(categoria.Id);
                return Page();
            }
            return NotFound();
        }
        public async Task<IActionResult> OnPostAddToCart(int idProd, int idCat)
        { 
            var product = new Product();
            product = await Task.Run(() => _productRepository.GetProductById(idProd));
            if (SessionHelper.GetObject<Cart>(HttpContext.Session, "CART") == null)
            {
                var carrito = new Cart();
                carrito.Total = (decimal)product.Price;
                carrito.Status = "PENDING";
                carrito.Products = new List<Product>();
                carrito.Products.Add(product);
                SessionHelper.SetObject(HttpContext.Session, "CART", carrito);
            }
            else
            {
                var cart = SessionHelper.GetObject<Cart>(HttpContext.Session, "CART");
                cart.Total = cart.Total + (decimal)product.Price;
                cart.Products.Add(product);
                SessionHelper.SetObject(HttpContext.Session, "CART", cart);
            }
            return await this.OnGet(idCat);
        }
    }
}