using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async void OnGet()
        {
            using (var client = new HttpClient { BaseAddress = new Uri("https://localhost:5001") })
            {
                var response = await client.GetStringAsync("products");
                _logger.LogInformation(response);
            }
        }
    }
}
