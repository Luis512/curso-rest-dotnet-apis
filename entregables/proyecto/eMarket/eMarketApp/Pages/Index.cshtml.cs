using eMarketApp.Repositories;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMarketApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public List<Product> Lista { get; set; } = new List<Product>();

        public string SearchProduct { get; set; }

        public IndexModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task OnGetAsync(string SearchProduct)
        {
            Lista = await _productRepository.GetProducts();

            if (!string.IsNullOrEmpty(SearchProduct))
            {
                Lista = Lista.Where(_ => _.Name.ToLower().Contains(SearchProduct.ToLower())).ToList();
            }
        }
    }
}
