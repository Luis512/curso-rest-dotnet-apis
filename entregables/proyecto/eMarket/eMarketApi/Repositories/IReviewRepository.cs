using eMarketDomain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eMarketApi.Repositories
{
    public interface IReviewRepository
    {
        List<Review> GetReviewByProduct(int id);

        Review Get(int id);

        Task Post(Review review);
    }
}
