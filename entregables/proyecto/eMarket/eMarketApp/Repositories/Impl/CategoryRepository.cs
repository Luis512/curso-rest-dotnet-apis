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
    public class CategoryRepository : ICategoryRepository
    {
        public ApiSettings _endpoints { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endpoints"></param>
        public CategoryRepository(IOptions<ApiSettings> endpoints)
        {
            _endpoints = endpoints.Value;
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="Categoria"/>.</returns>
        public async Task<List<Category>> GetCategories()
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_endpoints.BaseEndpoint) })
            {
                var response = await client.GetStringAsync(_endpoints.Endpoints.Categoria);
                return JsonConvert.DeserializeObject<List<Category>>(response);
            }
        }

        public async Task<Category> GetCategoryById(int id)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_endpoints.BaseEndpoint) })
            {
                var response = await client.GetStringAsync($"{_endpoints.Endpoints.Categoria}/{id}");
                return JsonConvert.DeserializeObject<Category>(response);
            }
        }

        public async Task<bool> AddCategory(Category category)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_endpoints.BaseEndpoint) })
            {
                var httpContent = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                var response = await client.PostAsync(_endpoints.Endpoints.Categoria, httpContent);
                if (response.StatusCode != HttpStatusCode.Created)
                    return false;
                return true;
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_endpoints.BaseEndpoint) })
            {
                var response = await client.DeleteAsync($"{_endpoints.Endpoints.Categoria}/{id}");
                if (response.StatusCode != HttpStatusCode.OK)
                    return false;
                return true;
            }
        }
    }
}
