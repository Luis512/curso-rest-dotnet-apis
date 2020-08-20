using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eMarketApp.Pages.Administracion.Edit
{
    [Authorize]
    public class CategoryModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}