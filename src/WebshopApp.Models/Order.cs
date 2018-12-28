using System;
using System.Collections.Generic;
using System.Text;

namespace WebshopApp.Models
{
    public class Order
    {
        public string Id { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        public Status Status { get; set; } = Status.Active;

        public string ClientId { get; set; }
        public virtual WebShopUser Client { get; set; }
    }
}
