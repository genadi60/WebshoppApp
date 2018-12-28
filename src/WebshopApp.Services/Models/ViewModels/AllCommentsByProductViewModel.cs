using System;
using System.Collections.Generic;
using System.Text;

namespace WebshopApp.Services.Models.ViewModels
{
    public class AllCommentsByProductViewModel
    {
        public AllCommentsByProductViewModel()
        {
            CommentViewModels = new HashSet<CommentViewModel>();
        }

        public IEnumerable<CommentViewModel> CommentViewModels { get; set; }
    }
}
