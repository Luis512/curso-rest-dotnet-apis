using System.Collections.Generic;

namespace eMarketDomain.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public double? Total { get; set; }
        public string Status { get; set; }

        public List<Product> Products { get; set; }
    }
}
