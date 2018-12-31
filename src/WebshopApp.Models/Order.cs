using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebshopApp.Models
{
    public class Order
    {
        public string Id { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public string ClientId { get; set; }
        public virtual WebShopUser Client { get; set; }

        public string ShipmentDataId { get; set; }
        public ShipmentData ShipmentData { get; set; }
    }
}
