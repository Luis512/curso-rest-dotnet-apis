using System;
using System.Collections.Generic;

namespace eMarketDomain.Models
{
    public class Order
    {
        public Order(decimal id, decimal? total, DateTime? processDate)
        {
            Id = id;
            Total = total;
            ProcessDate = processDate;
        }
        public decimal Id{ get; set; }

        public decimal? Total { get; set; }

        public DateTime? ProcessDate { get; set; }

        public List<Product> Products { get; set; }
    }
}
