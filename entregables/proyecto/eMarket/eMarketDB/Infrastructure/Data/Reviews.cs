using System;
using System.Collections.Generic;

namespace eMarketDB.Infrastructure.Data
{
    public partial class Reviews
    {
        public decimal Id { get; set; }
        public string Description { get; set; }
        public decimal? Rating { get; set; }
        public decimal IdUser { get; set; }
        public decimal IdProduct { get; set; }

        public virtual Products IdProductNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
