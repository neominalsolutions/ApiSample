using ApiSample.Dtos;
using ApiSample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokensController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Token([FromBody] AuthDto model)
        {
            if(model.UserName == "mert" && model.Password == "1234" && model.GrantType == GrantTypes.Password)
            {
                // access token da saklanacak bilgiler.
                var claims = new List<Claim>
                {
                    new Claim("sub","1"),
                    new Claim("username","mert")
                };

                return Ok(await _tokenService.GenerateToken(claims));
            }

            // eğer sistemde kayıtlı bir kullanıcı değilsek 401 hatası döndür.
            return Unauthorized();
        }
    }
}
