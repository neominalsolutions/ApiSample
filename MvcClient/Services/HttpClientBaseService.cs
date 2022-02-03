using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MvcClient.Services
{
    

    public  class HttpClientBaseService:IHttpClientService
    {
        private readonly HttpClient _httpClient;
        public HttpClientBaseService(IHttpClientFactory httpClientFactory, string clientName)
        {
            _httpClient = httpClientFactory.CreateClient(clientName);
        }

        public virtual async Task<TResponse> GetAsync<TResponse>(string endpoint)
        {
            var request = await _httpClient.GetAsync(endpoint);

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();

                // bir nesneyi alırken deserialize olarak alacağız.
                var model = JsonConvert.DeserializeObject<TResponse>(jsonString);

                return await Task.FromResult(model);
            }

            throw new Exception("HttpClient Get Error");
        }

        public Task<TResponse> PostAsync<TResponse, TRequest>(string endpoint, TRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
