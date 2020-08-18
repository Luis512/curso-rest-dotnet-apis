using System;
using System.Collections.Generic;

namespace eMarketDB.Infrastructure.Data
{
    public partial class Products
    {
        public Products()
        {
            ProductByOrder = new HashSet<ProductByOrder>();
            Reviews = new HashSet<Reviews>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public decimal? Stock { get; set; }
        public decimal IdCategory { get; set; }
        public decimal? Price { get; set; }

        public virtual Categories IdCategoryNavigation { get; set; }
        public virtual ICollection<ProductByOrder> ProductByOrder { get; set; }
        public virtual ICollection<Reviews> Reviews { get; set; }
    }
}
