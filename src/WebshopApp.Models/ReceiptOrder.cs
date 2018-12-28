using System;
using System.Collections.Generic;
using System.Text;

namespace WebshopApp.Models
{
    public class ReceiptOrder
    {
        public string Id { get; set; }

        public string ReceiptId { get; set; }
        public virtual Receipt Receipt { get; set; }

        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
