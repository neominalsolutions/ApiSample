using ApiSample.Dtos;
using ApiSample.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthenticatedUserService userService;

        public AuthController(IAuthenticatedUserService authenticatedUserService)
        {
            userService = authenticatedUserService;
        }


        [HttpPost("client-credentials")]
        public IActionResult ValidateClientCredentials([FromHeader] ClientHeadersDto model)
        {
            // [FromBody] attribute ile api ya gönderilen dosyayı application-json formatında alırız.
            // eğer www.urlformencoded şeklinde alıcak isek FromForm kullanırız.
            // Farklı şekillerde post ile veri taşınabilir.
            // FromHeader, FromQuery, FromRoute, FromBody, FromForm vs.

            // eğer client_credentials gibi api'ya bir konfigürasyon amaçlı data göndericek isek bu durumda ise FromHeader kullanırız. FromHeader'dan gönderilen veriler için ok tipinde result döner.

            return Ok(model);
        }

        [HttpGet("client-id")]
        public IActionResult GetClientInfo([FromHeader] string clientId)
        {
            // http get işleminde eğer hassas bir bilgi taşımayacak isek get kullanarak da httpheader üzerinden veri okuyabiliriz.
            return Ok("x-secret");
        }


        [Authorize]
        [HttpGet("test")]
        public IActionResult TestAuth()
        {

            //userService.GetUser.Email;

           return Ok();
        }


        [HttpPost("user-info")][Authorize]
        public IActionResult GetProfile([FromBody] UserInfoParam userInfoParam)
        {


           return  Ok();

            // access token üretildiği anda gelen token'a göre kimin authenticated olduğu httpcontext üzerindne öğrenebiliyoruz.
           //string id =   HttpContext.User.Claims.First(x => x.Type == "sub").Value;

            //return Ok(new CustomProfileDto()
            //{
            //    Email = "test@test.com",
            //    Name = "test-user",
            //    PhoneNumber = "4324324234",
            //    Roles = new string[] { "admin", "manager" }
            //});
        }

       

        [HttpGet("client-info/{clientId}/{clientSecret}")]
        //[HttpGet("client-info")]
        public IActionResult GetClientInfo2(string clientId, string clientSecret)
        {
            // http get işleminde eğer hassas bir bilgi taşımayacak isek get kullanarak da httpheader üzerinden veri okuyabiliriz.
            // httpget default da ya fromRoute yada formQuery yaptığımızdan dolayı işaretlemeye gerek yoktur.
            return Ok("x-secret");
        }


        [HttpGet("client-info")]
        //[HttpGet("client-info")]
        public IActionResult GetClientInfo3(string clientId, string clientSecret)
        {
            // http get işleminde eğer hassas bir bilgi taşımayacak isek get kullanarak da httpheader üzerinden veri okuyabiliriz.
            // httpget default da ya fromRoute yada formQuery yaptığımızdan dolayı işaretlemeye gerek yoktur.
            return Ok("x-secret");
        }
    }
}
