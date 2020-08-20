using System;
using System.Collections.Generic;

namespace eMarketDB.Infrastructure.Data
{
    public partial class Categories
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
