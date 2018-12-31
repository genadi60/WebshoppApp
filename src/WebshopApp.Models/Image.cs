using System;
using System.ComponentModel.DataAnnotations;
using WebshopApp.Models.Base;

namespace WebshopApp.Models
{
    
    public class Image : BaseModel<int>
    {
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }

        public string ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
