using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WebshopApp.Models
{
    public class Cart
    {
        public Cart()
        {
            Products = new List<Product>();
        }

        public string Id { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
