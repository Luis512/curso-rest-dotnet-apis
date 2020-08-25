using eMarketApp.Repositories;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.ComponentModel;
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
            _logger.LogWarning($"---------------------1. {product.Id}");
            _logger.LogWarning($"---------------------1.1. {product.Price}");
            var result = await _productRepository.UpdateProduct(product);
            _logger.LogWarning($"---------------------2. {result}");
            _logger.LogWarning($"---------------------3. {product.Id}");
            if (result)
                return await OnGet(product.Id);
            return BadRequest();
        }
    }
}