using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using eMarketApp.Repositories;
using Models = eMarketDomain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace eMarketApp.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public Models.Usuario loginUser { get; set; }

        public IndexModel(IUserRepository userRepository, ILogger<IndexModel> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public IActionResult OnGet()
        {
            try
            {
               
                if (this.User.Identity.IsAuthenticated)
                {
                    _logger.LogWarning($"[OnGet] User logged: {User.Identity.Name}");
                    return this.RedirectToPage("/Home");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            return Page();
        }

        public async Task<ActionResult> OnPostLogin()
        {
            var response = await _userRepository.LoginAsync(loginUser);
            _logger.LogWarning($"Is user logged? {response}");
            if (response)
            {
                await SignInUser(loginUser.Username, true);
                return this.RedirectToPage("/Home");
            }
            return RedirectToPage("/Error");
        }

        private async Task SignInUser(string username, bool isPersistent)
        {

            var claims = new List<Claim>();

            try
            {
                claims.Add(new Claim(ClaimTypes.Name, username));
                var claimIdenties = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimPrincipal = new ClaimsPrincipal(claimIdenties);
                var authenticationManager = Request.HttpContext; 
                await authenticationManager.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, new AuthenticationProperties() { IsPersistent = isPersistent });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}