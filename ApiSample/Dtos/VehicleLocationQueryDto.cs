using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSample.Dtos
{
    public class VehicleLocationQueryDto
    {

        [FromQuery]
        // enlem
        public string Latitude { get; set; }

        [FromQuery]
        // boylam
        public string Longitude { get; set; }

        


    }
}
