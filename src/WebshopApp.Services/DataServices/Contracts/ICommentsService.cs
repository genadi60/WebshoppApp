using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopApp.Services.Models.InputModels;
using WebshopApp.Services.Models.ViewModels;

namespace WebshopApp.Services.DataServices.Contracts
{
    public interface ICommentsService
    {
        IEnumerable<CommentViewModel> GetAll();

        IEnumerable<CommentViewModel> GetAllByProduct(int id);

        Task<int> Add(CreateCommentInputModel model);

        Task<int> Edit(CommentViewModel model);

        Task<int> Delete(int id);

        CommentViewModel GetById(int id);
    }
}
