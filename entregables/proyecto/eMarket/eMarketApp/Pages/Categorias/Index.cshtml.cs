using eMarketApp.Repositories;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eMarketApp.Pages.Categoria
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;

        public List<Category> Lista { get; set; } = new List<Category>();

        public IndexModel(ICategoryRepository categoriaRepository)
        {
            _categoryRepository = categoriaRepository;
        }
        public async Task<IActionResult> OnGet()
        {
            Lista = await _categoryRepository.GetCategories();
            return Page();
        }
    }
}