using eMarketApp.Helpers;
using eMarketApp.Repositories;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eMarketApp.Pages.User
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<IndexModel> _logger;

        public Usuario Usuario { get; set; }

        public List<Order> PastOrders { get; set; }

        public IndexModel(IUserRepository userRepository, ICartRepository cartRepository, IProductRepository productRepository, ILogger<IndexModel> logger)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _logger = logger;
            _productRepository = productRepository;
        }
        public async Task<ActionResult> OnGet()
        {
            Usuario = await _userRepository.GetInformation(User.Identity.Name);
            PastOrders = await _cartRepository.GetPastOrders(User.Identity.Name);
            return Page();
        }

        public async Task<ActionResult> OnPostSignOff()
        {
            try
            {
                var authenticationManager = Request.HttpContext; 
                await authenticationManager.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                SessionHelper.CleanCart(HttpContext.Session);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return this.RedirectToPage("/Index");
        }

        public async Task<PartialViewResult> OnGetOrderModalPartial(int id)
        {
            var model = new Order(id, 123, DateTime.Now);
            model.Products = await _productRepository.GetProductsByOrder(id);
            return new PartialViewResult
            {
                ViewName = "OrderLightbox",
                ViewData = new ViewDataDictionary<Order>(ViewData, model),
            };
        }
    }
}