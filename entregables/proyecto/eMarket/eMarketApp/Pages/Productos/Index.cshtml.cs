using eMarketApp.Repositories;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eMarketApp.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public List<Product> Lista { get; set; } = new List<Product>();

        public IndexModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IActionResult> OnGet()
        {
            Lista = await _productRepository.GetProducts();
            return Page();
        }
    }
}