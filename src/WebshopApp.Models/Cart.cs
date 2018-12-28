using System;
using System.Collections.Generic;
using System.Text;

namespace WebshopApp.Models
{
    public class Cart
    {
        public Cart()
        {
            Orders = new HashSet<Order>();
        }

        public string Id { get; set; }

        public virtual IEnumerable<Order> Orders { get; set; }
    }
}
