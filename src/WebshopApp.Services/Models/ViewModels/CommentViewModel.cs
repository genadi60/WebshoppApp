using Microsoft.AspNetCore.Identity;
using WebshopApp.Models;
using WebshopApp.Services.MappingServices;
using WebshopApp.Services.Models.InputModels;

namespace WebshopApp.Services.Models.ViewModels
{
    public class CommentViewModel : IMapFrom<Comment>, IMapTo<CreateCommentInputModel>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }
        public virtual WebShopUser User { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
