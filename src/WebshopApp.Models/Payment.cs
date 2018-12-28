using System;
using System.Collections.Generic;
using System.Text;

namespace WebshopApp.Models
{
    public class Payment
    {
        public string Id { get; set; }

        public decimal Cost { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public DateTime PaidOn { get; set; }
    }
}
