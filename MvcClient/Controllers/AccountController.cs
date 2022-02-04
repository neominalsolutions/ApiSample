using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MvcClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
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


                // Post işlemleri

                var handler = new JwtSecurityTokenHandler();
                 var jwtToken =  handler.ReadJwtToken(tokenResponse.AccessToken);

                int expireDateSeconds = (int)jwtToken.Payload.Exp;
                // user hesabına ait hesap için geçerli claim bilgileri saklanır.
                var claimPrinciple = new ClaimsPrincipal();

                var identity = new ClaimsIdentity(jwtToken.Payload.Claims, "ExternalAuth");
                claimPrinciple.AddIdentity(identity);

                // kimlik doğrulanırken saklanacak olan değerler özellikler
                // burada token, token süresi, token kalıcı olup olmaycağı gibi bilgiler saklarnır.
                var authProperties = new AuthenticationProperties();
                authProperties.IsPersistent = model.RememberMe;
                authProperties.ExpiresUtc = DateTimeOffset.FromUnixTimeSeconds(expireDateSeconds);



                var accessToken = new AuthenticationToken();
                accessToken.Name = "AccessToken";
                accessToken.Value = tokenResponse.AccessToken;

                var refreshToken = new AuthenticationToken();
                refreshToken.Name = "RefreshToken";
                refreshToken.Value = tokenResponse.RefreshToken;

                var tokens = new List<AuthenticationToken>();
                tokens.Add(accessToken);
                tokens.Add(refreshToken);

                // uygulamada HttpContext üzerinde access ve refresh token saklayacağız ki httpcontext üzerinden bu token bilgilerine ulaşıp request atarken request header'a bu bilgileri gömeceğiz.

                authProperties.StoreTokens(tokens);
                // httpcontext üzerinde token sakladığımız method. Inmemory olarak saklar. Uygulama çalıştığı sürece saklanır. session bazlı tutar.
                
 
                await HttpContext.SignInAsync("ExternalAuth",claimPrinciple, authProperties);

                return Redirect("/");
            }


            ViewBag.Message = "Kullanıcı Hesabı doğrulanamadı!";

            return View();


        }


    }
}
