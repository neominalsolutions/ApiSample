using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSample.Dtos
{
    public class UserInfoParam
    {
        public string sub { get; set; } // kullanıcı id
        public IDictionary<string, string> requestedScopes = new Dictionary<string, string>();
    }
}
