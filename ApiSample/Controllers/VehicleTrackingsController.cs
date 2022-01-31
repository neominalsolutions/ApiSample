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
    public class VehicleTrackingsController : ControllerBase
    {

        [HttpPost("track-location")]
        public IActionResult Track([FromQuery] VehicleLocationQueryDto vehicleLocationDto, [FromHeader] string trackId)
        {
            // genelde önemli olmayan verileri query string yada route üzerinden gönderme eğilimindeyiz. ama sensitive olan verileri header yada body üzerinden taşırız.
            // https://localhost:5001/api/vehicletrackings/track-location?latitude=21&longitude=34
            return Ok();
        }

    }
}
