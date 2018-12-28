using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebshopApp.Data.Common;
using WebshopApp.Models;
using WebshopApp.Services.DataServices.Contracts;
using WebshopApp.Services.MappingServices;
using WebshopApp.Services.Models.InputModels;
using WebshopApp.Services.Models.ViewModels;

namespace WebshopApp.Services.DataServices
{
    public class CommentsService : ICommentsService
    {
        private readonly IRepository<Comment> commentsRepository;

        public CommentsService(IRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public AllCommentsByProductViewModel GetAllByProduct(int id)
        {
            var comments = this.commentsRepository.All()
                .Where(c => c.ProductId == id)
                .To<CommentViewModel>()
                .ToList();

            var model = new AllCommentsByProductViewModel
            {
                CommentViewModels = comments
            };

            return model;
        }

        public async Task<int> Add(CreateCommentInputModel model)
        {
            var comment = new Comment
            {
                Content = model.Content,
                ProductId = model.ProductId,
                UserId = model.UserId
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();

            var id = commentsRepository.All().Single(c => c.Content.Equals(model.Content)).Id;

            return id;
        }

        public async Task<int> Edit(CommentViewModel model)
        {
            var comment = commentsRepository.All()
                .FirstOrDefault(x => x.Id == model.Id); ;

            if (comment == null)
            {
                throw new KeyNotFoundException();
            }

            //comment.Id = id;
            //comment.ProductId = model.ProductId;
            //comment.UserId = model.UserId;
            comment.Content = model.Content;

            var id = await this.commentsRepository.Update(comment);

            return comment.Id;
        }

        public async Task<int> Delete(int id)
        {
            var comment = this.commentsRepository.All()
                .FirstOrDefault(c => c.Id == id);

            if (comment == null)
            {
                throw new KeyNotFoundException();
            }

            var index = await this.commentsRepository.Delete(comment);

            return index;
        }

        public CommentViewModel GetById(int id)
        {
            var model = commentsRepository.All()
                .Where(c => c.Id == id)
                .To<CommentViewModel>()
                .FirstOrDefault();

            return model;  
        }
    }
}
