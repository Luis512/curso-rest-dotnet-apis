using eMarketApp.Repositories;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace eMarketApp.Pages.Administracion.Edit
{
    [Authorize]
    public class ProductModel : PageModel
    {

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public Product product { get; set; }
        public Category productCategory { get; set; }

        public ProductModel(IProductRepository productRepository, ICategoryRepository categoryRepository)
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

        //public ActionResult OnPost()
        //{

        //}
    }
}