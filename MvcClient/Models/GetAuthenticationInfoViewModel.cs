using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcClient.Models
{
    public class GetAuthenticationInfoViewModel
    {
        public IEnumerable<Claim> UserClaims { get; set; }
        public IDictionary<string,string> AuthProperties { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string AuthenticatedUserName { get; set; }
        public string AuthenticatedUserId { get; set; }


    }
}
