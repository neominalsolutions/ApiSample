using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcClient.Consts;
using MvcClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MvcClient.Controllers
{
    public class HomeController : HttpClientController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,IHttpClientFactory httpClientFactory):base(httpClientFactory)
        {
            _logger = logger;
        }

        //private readonly HttpClient apiSampleClient;

        //public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        //{
        //    _logger = logger;
        //    apiSampleClient = httpClientFactory.CreateClient(HttpClientNames.ApiSample);
        //}

        public async Task<IActionResult> Index()
        {
            // aşağıdaki yöntem yerine httpClientFactory denilen bir yöntem ile controller bazlı client instance alacağız
            //using (HttpClient c = new HttpClient())
            //{
            //    c.BaseAddress = new Uri("https://localhost:5001");

            //}

           var request =  await apiSampleClient.GetAsync("api/products/v1");

            if (request.IsSuccessStatusCode) // eğer istek başarılı ise
            {
               var jsonString =  await request.Content.ReadAsStringAsync();

                // bir nesneyi alırken deserialize olarak alacağız.
                var model = JsonConvert.DeserializeObject<List<ProductViewModel>>(jsonString);
                
            }
            else
            {
                throw new Exception("Connection Failed");
            }


            return View();
        }

        public IActionResult Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
           
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
