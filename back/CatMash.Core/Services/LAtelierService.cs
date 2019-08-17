using CatMash.Core.Dto;
using CatMash.Core.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CatMash.Core.Services
{
    public class LAtelierService
    {
        public HttpClient Client { get; }

        public LAtelierService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://latelier.co/");
            Client = client;
        }

        public async Task<ImportDto> ImportCats()
        {
            var response = await Client.GetAsync("/data/cats.json");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<ImportDto>();
            return result;
        }
    }
}
