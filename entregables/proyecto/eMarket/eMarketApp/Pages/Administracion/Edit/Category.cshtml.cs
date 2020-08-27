using eMarketApp.Repositories;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace eMarketApp.Pages.Administracion.Edit
{
    [Authorize]
    public class CategoryModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;

        [BindProperty]
        public Category category { get; set; }

        public CategoryModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            category = new Category();
            category = await _categoryRepository.GetCategoryById(id);           
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateCategory()
        {
            try
            {
                var result = await _categoryRepository.UpdateCategory(category);
                if (result)
                    ViewData.Add("UpdateCat", true);
                else
                    ViewData.Add("UpdateCat", false);

            }
            catch (System.Exception)
            {
                ViewData.Add("UpdateCat", false);
            }
            return await OnGet(category.Id);
        }
    }
}