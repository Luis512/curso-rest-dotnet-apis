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
    public class ProductRepository : IProductRepository
    {
        public ApiSettings _endpoints { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endpoints"></param>
        public ProductRepository(IOptions<ApiSettings> endpoints)
        {
            _endpoints = endpoints.Value;
        }

        /// <summary>
        /// Gets all the products
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="Product"/>.</returns>
        public async Task<List<Product>> GetProducts()
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_endpoints.BaseEndpoint) })
            {
                var response = await client.GetStringAsync(_endpoints.Endpoints.Producto);
                return JsonConvert.DeserializeObject<List<Product>>(response);
            }
        }

        /// <summary>
        /// Gets a product by id.
        /// </summary>
        /// <param name="id">Product ID.</param>
        /// <returns>A <see cref="Product"/> object.</returns>
        public async Task<Product> GetProductById(int id)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_endpoints.BaseEndpoint) })
            {
                var response = await client.GetStringAsync($"{_endpoints.Endpoints.Producto}/{id}");
                return JsonConvert.DeserializeObject<Product>(response);
            }
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="product">A <see cref="Product"/> object.</param>
        /// <returns></returns>
        public async Task<bool> AddProduct(Product product)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_endpoints.BaseEndpoint) })
            {
                var httpContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                var response = await client.PostAsync(_endpoints.Endpoints.Producto, httpContent);
                return response.StatusCode == HttpStatusCode.Created;
            }
        }

        /// <summary>
        /// Deletes a product by id.
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>True </returns>
        public async Task<bool> DeleteProduct(int id)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_endpoints.BaseEndpoint) })
            {
                var response = await client.DeleteAsync($"{_endpoints.Endpoints.Producto}/{id}");
                return response.StatusCode == HttpStatusCode.OK;
            }
        }

        public async Task<List<Product>> GetProductsByCategory(int id)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_endpoints.BaseEndpoint) })
            {
                var response = await client.GetStringAsync($"{_endpoints.Endpoints.Producto}/ByCat/{id}");
                return JsonConvert.DeserializeObject<List<Product>>(response);
            }
        }

        public async Task<List<Product>> GetProductsByOrder(int id)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_endpoints.BaseEndpoint) })
            {
                var response = await client.GetStringAsync($"{_endpoints.Endpoints.Producto}/ByOrder/{id}");
                return JsonConvert.DeserializeObject<List<Product>>(response);
            }
        }
    }
}
