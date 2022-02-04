using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSample.Dtos
{
    public class CustomProfileDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
        public string PhoneNumber { get; set; }

    }
}
