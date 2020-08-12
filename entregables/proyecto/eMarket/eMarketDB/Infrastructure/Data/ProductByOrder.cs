using System;
using System.Collections.Generic;

namespace eMarketDB.Infrastructure.Data
{
    public partial class ProductByOrder
    {
        public decimal IdOrder { get; set; }
        public decimal IdProduct { get; set; }

        public virtual Orders IdOrderNavigation { get; set; }
        public virtual Products IdProductNavigation { get; set; }
    }
}
