using System.ComponentModel.DataAnnotations;
using WebshopApp.Models;
using WebshopApp.Services.MappingServices;
using WebshopApp.Services.Models.ViewModels;

namespace WebshopApp.Services.Models.InputModels
{
    public class EditCommentInputModel: IMapFrom<Comment>, IMapTo<CommentViewModel>, IMapTo<Comment>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Content { get; set; }
        
        [Required]
        public string UserId { get; set; }
        public virtual WebShopUser User { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
