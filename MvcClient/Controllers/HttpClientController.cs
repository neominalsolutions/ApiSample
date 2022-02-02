using Microsoft.AspNetCore.Mvc;
using MvcClient.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MvcClient.Controllers
{
    public abstract class HttpClientController : Controller
    {
        protected readonly HttpClient apiSampleClient;
        //protected readonly HttpClient apiSampleProductClient;

        public HttpClientController(IHttpClientFactory httpClientFactory)
        {
            apiSampleClient = httpClientFactory.CreateClient(HttpClientNames.ApiSample);
            //apiSampleProductClient = httpClientFactory.CreateClient(HttpClientNames.ProductApi);
        }


    }
}
