using System;
using System.Collections.Generic;
using System.Linq;
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

        public string ClientId { get; set; }
        public WebShopUser Client { get; set; }

        public virtual IEnumerable<Order> Orders { get; set; }

        public void Add(Order order)
        {
            Orders.Append(order);
        }
    }
}
