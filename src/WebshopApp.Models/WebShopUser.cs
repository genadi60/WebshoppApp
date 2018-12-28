using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace WebshopApp.Models
{
    public class WebShopUser : IdentityUser
    {
        public WebShopUser()
        {
            ClientReceipts = new HashSet<ClientReceipt>();
        }

        [Required]
        public string RoleId { get; set; }

        public string Role { get; set; }

        public string CartId { get; set; }
        public virtual Cart Cart { get; set; }

        public virtual IEnumerable<ClientReceipt> ClientReceipts { get; set; }
    }
}
