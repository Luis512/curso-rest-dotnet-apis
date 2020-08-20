using eMarketDomain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eMarketApp.Repositories
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetReviews(int id);

        Task<bool> AddReview(Review review);
    }
}
