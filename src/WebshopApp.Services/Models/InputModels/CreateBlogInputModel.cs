using System.ComponentModel.DataAnnotations;
using WebshopApp.Models;
using WebshopApp.Services.MappingServices;

namespace WebshopApp.Services.Models.InputModels
{
    public class CreateBlogInputModel : IMapTo<Blog>
    {
        [Required]
        [MinLength(6)]
        public string Title { get; set; }

        [Required]
        [MinLength(50)]
        public string Content { get; set; }


    }
}
