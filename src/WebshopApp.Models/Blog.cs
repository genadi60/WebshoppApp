using System;
using System.ComponentModel.DataAnnotations;
using WebshopApp.Models.Base;

namespace WebshopApp.Models
{
    public class Blog : BaseModel<int>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(50)]
        public string Content { get; set; }

        public DateTime PostedOn { get; set; } = DateTime.UtcNow;

        public string PictureUri { get; set; }
    }
}
