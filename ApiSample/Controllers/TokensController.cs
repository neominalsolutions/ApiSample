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
    public class TokensController : ControllerBase
    {

        public async Task<IActionResult> Token([FromBody] AuthDto model)
        {

            return  Ok(await Task.FromResult(new TokenDto()));
        }
    }
}
