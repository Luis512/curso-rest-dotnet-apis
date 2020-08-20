using System;
using System.Collections.Generic;

namespace eMarketDB.Infrastructure.Data
{
    public partial class Orders
    {
        public Orders()
        {
            ProductByOrder = new HashSet<ProductByOrder>();
        }

        public decimal Id { get; set; }
        public decimal IdUser { get; set; }
        public decimal? Total { get; set; }
        public string Status { get; set; }
        public DateTime? ProcessDate { get; set; }

        public virtual User IdUserNavigation { get; set; }
        public virtual ICollection<ProductByOrder> ProductByOrder { get; set; }
    }
}
