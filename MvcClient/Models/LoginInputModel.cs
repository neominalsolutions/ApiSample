using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClient.Models
{
    public class LoginInputModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } // cookie persistent olacak

        public string GrantType { get; set; } = "password";


    }
}
