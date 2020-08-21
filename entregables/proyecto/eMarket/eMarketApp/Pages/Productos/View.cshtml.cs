using eMarketApp.Helpers;
using eMarketApp.Repositories;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMarketApp.Pages.Productos
{
    [Authorize]
    public class ViewModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<ViewModel> _logger;

        public List<SelectListItem> ratingDropdown { get; set; } = new List<SelectListItem>();

        public Product product { get; set; } 
        public Category productCategory { get; set; }

        public List<Review> reviews { get; set; }

        [BindProperty]
        public Review review { get; set; }

        public ViewModel(IProductRepository productRepository, ICategoryRepository categoryRepository, IReviewRepository reviewRepository, IUserRepository userRepository,  ILogger<ViewModel> logger)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
            _logger = logger;
            ratingDropdown = FillRatingDropdown();
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
                carrito.Total = (decimal?)product.Price;
                carrito.Status = "PENDING";                
                carrito.Products = new List<Product>();
                product.Quantity = 1;
                carrito.Products.Add(product);
                SessionHelper.SetObject(HttpContext.Session, "CART", carrito);
            }
            else
            {
                var cart = SessionHelper.GetObject<Cart>(HttpContext.Session, "CART");
                if (cart.Products.Any(p => p.Id == id))
                {
                    cart.Products = cart.Products.Where(p => p.Id == id).Select(u => { u.Quantity = u.Quantity + 1; return u; }).ToList();
                }
                else
                {
                    cart.Products.Add(product);
                }
                cart.Total = cart.Total + (decimal?)product.Price;
                SessionHelper.SetObject(HttpContext.Session, "CART", cart);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddReview(int id)
        {
            var user = await _userRepository.GetInformation(User.Identity.Name);
            if (user != null)
            {
                review.IdProduct = id;
                review.IdUser = user.Id;

                var response = await _reviewRepository.AddReview(review);
                if (response)
                {
                    review = new Review();
                    return await this.OnGet(id);
                }
            }
           return RedirectToPage("/Error");
        }

        private  List<SelectListItem> FillRatingDropdown()
        {
            var ratings = new List<SelectListItem>();
            for(int i = 1; i <= 5; i++)
            {
                ratings.Add(new SelectListItem($"{i} Estrella(s)", i.ToString()));
            }
            return ratings;
        }
    }
}