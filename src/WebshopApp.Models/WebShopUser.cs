using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace WebshopApp.Models
{
    public class WebShopUser : IdentityUser
    {
        [Required]
        public string RoleId { get; set; }

        public string Role { get; set; }
    }
}
