using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebshopApp.Models.Base;

namespace WebshopApp.Models
{
    public class Product : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
