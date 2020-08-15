using eMarketApp.Repositories;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace eMarketApp.Pages.User
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public Usuario Usuario { get; set; }

        public IndexModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ActionResult> OnGet()
        {
            Usuario = await _userRepository.GetInformation(User.Identity.Name);
            return Page();
        }

        public async Task<ActionResult> OnPostSignOff()
        {
            try
            {
                var authenticationManager = Request.HttpContext; 
                await authenticationManager.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return this.RedirectToPage("/Index");
        }
    }
}