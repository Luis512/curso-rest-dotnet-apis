using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public List<Product> Products { get; set; } = new List<Product>();

        public async Task<IActionResult> OnGet()
        {
            var webApi = System.Environment.GetEnvironmentVariable("ServerUrl");
            _logger.LogInformation($"===== {webApi}");
            using (var client = new HttpClient { BaseAddress = new Uri(webApi) })
            {
                var response = await client.GetStringAsync("products");
                ViewData["Products"] = JsonConvert.DeserializeObject<List<Product>>(response); 
            }
            return Page();
        }
    }
}
