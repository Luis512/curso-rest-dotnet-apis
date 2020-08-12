using eMarketApp.Helpers;
using eMarketApp.Repositories;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eMarketApp.Pages.Productos
{
    public class ViewModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly ILogger<ViewModel> _logger;

        public Product product { get; set; } 
        public Category productCategory { get; set; }

        public List<Review> reviews { get; set; }

        [BindProperty]
        public Review review { get; set; }

        public ViewModel(IProductRepository productRepository, ICategoryRepository categoryRepository, IReviewRepository reviewRepository, ILogger<ViewModel> logger)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _reviewRepository = reviewRepository;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet(int id)
        {
           
            product = await _productRepository.GetProductById(id);
            productCategory = await _categoryRepository.GetCategoryById(product.IdCategory);
            reviews = await _reviewRepository.GetReviews(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAddProductCart(int id)
        {
            product = new Product();
            product = await Task.Run(() => _productRepository.GetProductById(id));
            productCategory = await Task.Run(() => _categoryRepository.GetCategoryById(product.IdCategory));
            reviews = await _reviewRepository.GetReviews(id);

            if (SessionHelper.GetObject<Cart>(HttpContext.Session, "CART") == null)
            {
                var carrito = new Cart();
                carrito.Total += product.Price;
                carrito.Status = "PENDING";
                carrito.Products = new List<Product>();
                carrito.Products.Add(product);
                SessionHelper.SetObject(HttpContext.Session, "CART", carrito);
            }
            else
            {
                var cart = SessionHelper.GetObject<Cart>(HttpContext.Session, "CART");
                cart.Products.Add(product);
                SessionHelper.SetObject(HttpContext.Session, "CART", cart);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddReview(int id)
        {
            _logger.LogError($"---------{id}");

           review.IdProduct = id;
            review.IdUser = 123456;
            _logger.LogError($"---------{review.IdProduct} - {review.IdUser} - {review.Description}");
            var response = await _reviewRepository.AddReview(review);
            if (response)
            {
                return await this.OnGet(id);
            }
            return RedirectToPage("/Error");
        }
    }
}