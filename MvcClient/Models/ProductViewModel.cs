using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClient.Models
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

    }
}
