using System.ComponentModel.DataAnnotations;
using WebshopApp.Models.Base;

namespace WebshopApp.Models
{
    public class Image : BaseModel<int>
    {
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
