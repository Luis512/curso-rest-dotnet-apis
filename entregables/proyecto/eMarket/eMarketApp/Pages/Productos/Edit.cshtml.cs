using eMarketApp.Repositories;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace eMarketApp.Pages.Productos
{
    public class EditModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public Product product { get; set; }
        public Category productCategory { get; set; }

        public EditModel(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            product = new Product();
            product = await _productRepository.GetProductById(id);
            productCategory = await _categoryRepository.GetCategoryById(product.IdCategory);
            return Page();
        }
    }
}