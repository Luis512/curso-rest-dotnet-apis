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

        /// <summary>
        /// Gets an specifics category by its id
        /// </summary>
        /// <param name="id">Category's id/param>
        /// <returns></returns>
        public async Task<Category> GetCategoryById(int id)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_endpoints.BaseEndpoint) })
            {
                var response = await client.GetStringAsync($"{_endpoints.Endpoints.Categoria}/{id}");
                return JsonConvert.DeserializeObject<Category>(response);
            }
        }

        /// <summary>
        /// Creates a new category.
        ///  </summary>
        /// <param name="category"></param>
        /// <returns>True if the category was created succesfully, otherwise returns false.</returns>
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

        /// <summary>
        /// Deletes an specific category by its id.
        /// </summary>
        /// <param name="id">Category's id/param>
        /// <returns>True if the category was deleted succesfully, otherwise returns false.</returns>
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

        /// <summary>
        /// Updates an specific category
        /// </summary>
        /// <param name="category">A <see cref="Category"/> object.</param>
        /// <returns>True if the category was updated succesfully, otherwise returns false.</returns>
        public async Task<bool> UpdateCategory(Category category)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_endpoints.BaseEndpoint) })
            {
                var httpContent = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                var response = await client.PutAsync(_endpoints.Endpoints.Categoria, httpContent);
                return response.StatusCode == HttpStatusCode.Created;
            }
        }
    }
}
