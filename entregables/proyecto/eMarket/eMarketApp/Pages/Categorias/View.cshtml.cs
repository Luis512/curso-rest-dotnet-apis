using System.Collections.Generic;
using System.Threading.Tasks;
using eMarketApp.Repositories;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eMarketApp.Pages.Categorias
{
    public class ViewModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;

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
    }
}