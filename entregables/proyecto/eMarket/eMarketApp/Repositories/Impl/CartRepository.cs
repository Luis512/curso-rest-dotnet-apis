using eMarketDomain.Models;
using Microsoft.Extensions.Logging;
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
    public class CartRepository : ICartRepository
    {
        public ApiSettings _endpoints { get; set; }

        private readonly ILogger<CartRepository> _logger;

        public CartRepository(IOptions<ApiSettings> endpoints, ILogger<CartRepository> logger)
        {
            _endpoints = endpoints.Value;
            _logger = logger;
        }

        public async Task<bool> Checkout(Cart cart)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_endpoints.BaseEndpoint) })
            {
                var httpContent = new StringContent(JsonConvert.SerializeObject(cart), Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                var response = await client.PostAsync("cart", httpContent);
                return true;
            }
        }

        public async Task<List<Order>> GetPastOrders(string username)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_endpoints.BaseEndpoint) })
            {
                var response = await client.GetStringAsync($"cart/{username}");
                return JsonConvert.DeserializeObject<List<Order>>(response);
            }
        }
    }
}
