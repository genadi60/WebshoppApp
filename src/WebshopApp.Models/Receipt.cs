using System;
using System.Collections.Generic;
using System.Text;

namespace WebshopApp.Models
{
    public class Receipt
    {
        public Receipt()
        {
            ReceiptOrders = new HashSet<ReceiptOrder>();
        }

        public string Id { get; set; }

        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        public decimal Total { get; set; }

        public string PaymentId { get; set; }
        public virtual Payment Payment { get; set; }

        public string ClientId { get; set; }
        public virtual WebShopUser Client { get; set; }

        public string CashierId { get; set; }
        public virtual WebShopUser Cashier { get; set; }

        public virtual IEnumerable<ReceiptOrder> ReceiptOrders { get; set; }
    }
}
