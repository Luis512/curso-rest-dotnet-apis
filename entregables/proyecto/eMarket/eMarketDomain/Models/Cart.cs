using System.Collections.Generic;

namespace eMarketDomain.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public decimal? Total { get; set; }
        public string Status { get; set; }

        public List<Product> Products { get; set; }
    }
}
