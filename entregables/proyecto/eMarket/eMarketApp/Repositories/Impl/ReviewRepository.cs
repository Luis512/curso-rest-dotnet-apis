using eMarketDomain.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace eMarketApp.Repositories.Impl
{
    public class ReviewRepository : IReviewRepository
    {
        public ApiSettings _endpoints { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endpoints"></param>
        public ReviewRepository(IOptions<ApiSettings> endpoints)
        {
            _endpoints = endpoints.Value;
        }

        public async Task<List<Review>> GetReviews(int id)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_endpoints.BaseEndpoint) })
            {
                var response = await client.GetStringAsync($"{_endpoints.Endpoints.Review}/ByProd/{id}");
                return JsonConvert.DeserializeObject<List<Review>>(response);
            }
        }

        public async Task<bool> AddReview(Review review)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_endpoints.BaseEndpoint) })
            {
                var httpContent = new StringContent(JsonConvert.SerializeObject(review), Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                var response = await client.PostAsync(_endpoints.Endpoints.Review, httpContent);
                return response.StatusCode == HttpStatusCode.Created;
            }
        }
    }
}
