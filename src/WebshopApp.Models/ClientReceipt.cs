using System;
using System.Collections.Generic;
using System.Text;

namespace WebshopApp.Models
{
    public class ClientReceipt
    {
        public string Id { get; set; }

        public string ClientId { get; set; }
        public virtual WebShopUser Client { get; set; }

        public string ReceiptId { get; set; }
        public virtual Receipt Receipt { get; set; }
    }
}
