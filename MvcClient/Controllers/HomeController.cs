using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcClient.Consts;
using MvcClient.Extensions;
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
        private readonly HttpClient _apiSampleClient;


        public HomeController(ILogger<HomeController> logger,IHttpClientFactory httpClientFactory) :base(httpClientFactory)
        {
            _logger = logger;
            _apiSampleClient = httpClientFactory.CreateClient(HttpClientNames.ApiSample);


        }

        [Authorize]
        public async Task<ActionResult> GetAuthenticateInfo()
        {
            var result = await HttpContext.AuthenticateAsync();

            var model = new GetAuthenticationInfoViewModel
            {
                UserClaims = result.Principal.Claims,
                //AccessToken = result.Properties.GetTokenValue("AccessToken"),
                //RefreshToken = result.Properties.GetTokenValue("RefreshToken"),
                AuthenticatedUserId = result.Principal.Claims.First(x=> x.Type == "id").Value,
                AuthenticatedUserName = HttpContext.User.Identity.Name,
                AuthProperties = result.Properties.Items
            };

            return View(model);
        }

        public async Task<IActionResult> Index()
        {
            // aşağıdaki yöntem yerine httpClientFactory denilen bir yöntem ile controller bazlı client instance alacağız
            //using (HttpClient c = new HttpClient())
            //{
            //    c.BaseAddress = new Uri("https://localhost:5001");

            //}


            //var authResult = await HttpContext.AuthenticateAsync();
            //var accessToken = authResult.Properties.GetTokenValue("AccessToken");
            //var refreshToken = authResult.Properties.GetTokenValue("RefreshToken");

            //if (HttpContext.User.Identity.IsAuthenticated)
            //{
              
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

        public async Task<IActionResult> Index2()
        {

            
        // common layer altında bu arkadaşı kullandığımızda her projede tek satırla istediğimiz formatta httpResponse dönüşü alırız.
           var response =  await _apiSampleClient.GetAsync<List<ProductViewModel>>(EndpointNames.ProductsV1);
         
       
            // tüm http get ve http post istekleri için bu kullanılacaktır.
            //var response =  await _productClient.GetAsync<List<ProductViewModel>>(EndpointNames.ProductsV1);

            return View(response);
        }

        /// <summary>
        /// Sadece Authentica olduktan sonra Access Token ile istek yapmak için kullanıyoruz.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AuthorizedApiAction()
        {
            var param = new ProductViewModel
            {
                Id = "1",
                Name = "Kazak",
                Price = 10,
                Stock = 15
            };


            //var token = (await HttpContext.AuthenticateAsync()).Properties.GetTokenValue("AccessToken");

            //_apiSampleClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            //var response = await _apiSampleClient.PostAsync<ProductViewModel, ProductViewModel>(EndpointNames.AuthorizedEndPoint, param);

            // accesstoken olmalı yani login olmam gerekiyor.
            var response = await _apiSampleClient.PostAsync<ProductViewModel, ProductViewModel>(EndpointNames.AuthorizedEndPoint, param, HttpContext);

            if (response.IsSuccedeed)
            {

                return View();
            }

            return Unauthorized();
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
