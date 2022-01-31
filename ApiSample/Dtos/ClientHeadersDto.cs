using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSample.Dtos
{
    // headerdan okunacak olan post değerleri için formHeader attribute kullanıyoruz.
    // eğer bir şeyi post yada get istedeğinde header üzerinden okumak istiyorsak headerdan okunacak olan değerleri FromHeader attribute ile işaretleriz
    public class ClientHeadersDto
    {
        [FromHeader]
        public string ClientId { get; set; }

        [FromHeader]
        public string ClientSecret { get; set; }

    }
}
