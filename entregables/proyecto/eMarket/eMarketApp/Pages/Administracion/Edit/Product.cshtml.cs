using eMarketApp.Repositories;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace eMarketApp.Pages.Administracion.Edit
{
    [Authorize]
    public class ProductModel : PageModel
    {

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<ProductModel> _logger;

        [BindProperty]
        public Product product { get; set; }

        [BindProperty]
        public Category productCategory { get; set; }

        public ProductModel(IProductRepository productRepository, ICategoryRepository categoryRepository, ILogger<ProductModel> logger)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
            
        }

        public async Task<IActionResult> OnGet(int id)
        {
           
            product = new Product();
            product = await _productRepository.GetProductById(id);
            productCategory = await _categoryRepository.GetCategoryById(product.IdCategory);
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateProduct()
        {
            try
            {
                var result = await _productRepository.UpdateProduct(product);
                if (result)
                    ViewData.Add("UpdateProduct", true);
                else
                    ViewData.Add("UpdateProduct", false);
               
            }
            catch (System.Exception)
            {
                ViewData.Add("UpdateProduct", false);
            }
            return await OnGet(product.Id);
        }
    }
}