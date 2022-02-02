using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MvcClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MvcClient.Controllers
{
    public class AccountController : HttpClientController
    {
        public AccountController(IHttpClientFactory httpClientFactory):base(httpClientFactory)
        {

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel model)
        {

            var param = new
            {
                userName = model.UserName,
                password = model.Password,
                grantType = model.GrantType
            };

            var jsonString = JsonConvert.SerializeObject(param);
            var stringContent = new StringContent(jsonString, Encoding.UTF8,"application/json");


            var request = await apiSampleClient.PostAsync("api/tokens", stringContent);

            if (request.IsSuccessStatusCode)
            {
                var content = await request.Content.ReadAsStringAsync();

                var tokenResponse = JsonConvert.DeserializeObject<TokenViewModel>(content);

                // cookie oluşturma işlemleri yapmamız kullancıı HttpContext signIn yapmamız vs
                // token Decode edip içindeki claim bilgilerini almamız vs lazım.

               //var auth = await  HttpContext.AuthenticateAsync();
               // auth.Properties.

               // HttpContext.SignInAsync(,,)
            }

            return View();


        }
    }
}
