using MvcClient.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MvcClient.Services
{
    public class ProductHttpClientService : HttpClientBaseService, IProductHttpClientService
    {
        public ProductHttpClientService(IHttpClientFactory httpClientFactory, string clientName = HttpClientNames.ApiSample) : base(httpClientFactory, clientName)
        {
        }

        public override Task<TResponse> GetAsync<TResponse>(string endpoint)
        {
            return base.GetAsync<TResponse>(endpoint);
        }
    }
}
