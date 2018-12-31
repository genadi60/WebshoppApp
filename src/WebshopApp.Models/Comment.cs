using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WebshopApp.Models.Base;

namespace WebshopApp.Models
{
    [Serializable]
    public class Comment : BaseModel<int>
    {
        [Required]
        public string Content { get; set; }
        
        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual IdentityUser User { get; set; }
    }
}
