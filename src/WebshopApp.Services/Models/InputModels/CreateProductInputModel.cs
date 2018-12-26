using System.ComponentModel.DataAnnotations;
using WebshopApp.Services.MappingServices;
using WebshopApp.Services.Models.ViewModels;

namespace WebshopApp.Services.Models.InputModels
{
    public class CreateProductInputModel : IMapFrom<ProductViewModel>
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [MinLength(15)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public byte[] Image { get; set; }
    }
}
