using eMarketDomain.Models;
using System.Collections.Generic;

namespace eMarketApi.Repositories
{
    public interface IReviewRepository
    {
        List<Review> GetReviewByProduct(int id);

        Review Get(int id);

        void Post(Review review);
    }
}
