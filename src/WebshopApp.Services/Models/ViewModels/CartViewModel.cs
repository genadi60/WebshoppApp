using System;
using System.Collections.Generic;
using System.Text;
using WebshopApp.Models;

namespace WebshopApp.Services.Models.ViewModels
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            Orders = new HashSet<Order>();
        }

        public string Id { get; set; }

        public string ClientId { get; set; }

        public virtual IEnumerable<Order> Orders { get; set; }
    }
}
