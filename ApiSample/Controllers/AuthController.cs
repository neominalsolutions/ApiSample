using ApiSample.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
