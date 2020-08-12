using AutoMapper;
using eMarketDB.Infrastructure.Data;
using eMarketDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eMarketApi.Repositories.Impl
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IMapper _mapper;
        private readonly eMarketDBContext _context;

        public ReviewRepository(IMapper mapper, eMarketDBContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Review Get(int id)
        {
            var review = new Review();
            review = _mapper.Map<Review>(_context.Reviews.Where(_ => _.Id.Equals(id)).FirstOrDefault());
            return review;
        }

        public List<Review> GetReviewByProduct(int id)
        {
            var reviewsByProd = _context.Reviews.Where(_ => _.IdProduct.Equals(id));
            List<Review> reviews = new List<Review>();
            foreach (var review in reviewsByProd)
            {
                reviews.Add(_mapper.Map<Review>(review));
            }
            return reviews;
        }

        public async void Post(Review review)
        {
            Reviews newReview = _mapper.Map<Reviews>(review);
            newReview.Id = new Random().Next(1, 10000);
            _context.Reviews.Add(newReview);
            await _context.SaveChangesAsync();
        }
    }
}
