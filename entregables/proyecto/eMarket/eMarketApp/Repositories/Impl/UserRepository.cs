using eMarketDomain.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace eMarketApp.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {

        public ApiSettings _endpoints { get; set; }

        public UserRepository(IOptions<ApiSettings> endpoints)
        {
            _endpoints = endpoints.Value;
        }
        public async Task<bool> LoginAsync(Usuario user)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_endpoints.BaseEndpoint) })
            {
                var httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                var response = await client.PostAsync("user", httpContent);
                return response.StatusCode == HttpStatusCode.OK;
            }
        }

        public async Task<Usuario> GetInformation(string username)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_endpoints.BaseEndpoint) })
            {
                var response = await client.GetStringAsync($"user/{username}");
                return JsonConvert.DeserializeObject<Usuario>(response);
            }
        }
    }
}
