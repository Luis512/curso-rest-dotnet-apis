using System;
using System.Collections.Generic;

namespace eMarketDB.Infrastructure.Data
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Orders>();
            Reviews = new HashSet<Reviews>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Direction { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Reviews> Reviews { get; set; }
    }
}
