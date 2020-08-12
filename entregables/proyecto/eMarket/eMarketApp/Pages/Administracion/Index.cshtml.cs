using eMarketApp.Repositories;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eMarketApp.Pages.Administracion
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        public ICategoryRepository _categoryRepository { get; }
        public List<Category> categoryList { get; set; }
        public List<Product> productList { get; set; }
        
        [BindProperty]
        public Category category{ get; set; }

        [BindProperty]
        public Product product { get; set; }

        public List<SelectListItem> categoryDropdown { get; set; } = new List<SelectListItem>();

        public IndexModel(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<ActionResult> OnGet()
        {
            categoryList = await _categoryRepository.GetCategories();
            productList = await _productRepository.GetProducts();
            categoryDropdown = await FillMarcaDropdown(categoryList);
            return Page();

        }

        public async Task<ActionResult> OnPostAddProduct()
        {
            var response = await _productRepository.AddProduct(product);
            if (response)
            {
                ViewData["CurrentTab"] = "Product";
                return await this.OnGet();
            }
            return RedirectToPage("/Error");
        }

        public async Task<ActionResult> OnPostAddCategory()
        {           
            var response = await _categoryRepository.AddCategory(category);
            if (response)
            {
                ViewData["CurrentTab"] = "Category";
                return await this.OnGet();
            }
            return RedirectToPage("/Error");
        }

        
        public async Task<ActionResult> OnPostDeleteProduct(int id)
        {
            var response = await _productRepository.DeleteProduct(id);
            if (response)
            {
                ViewData["CurrentTab"] = "Category";
                return await this.OnGet();
            }
            return RedirectToPage("/Error");
        }

        public async Task<ActionResult> OnPostDeleteCategory(int id)
        {
            var response = await _categoryRepository.DeleteCategory(id);
            if (response)
            {
                ViewData["CurrentTab"] = "Category";
                return await this.OnGet();
            }
            return RedirectToPage("/Error");
        }

        private async Task<List<SelectListItem>> FillMarcaDropdown(List<Category> categories)
        {
            var categoriesAux = new List<SelectListItem>();
            foreach (var category in categories)
            {
                categoriesAux.Add(new SelectListItem(category.Name, category.Id.ToString()));
            }
            return categoriesAux;
        }
    }
}