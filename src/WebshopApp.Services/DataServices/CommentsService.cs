using System;
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
        private readonly IRepository<Comment> _commentsRepository;

        public CommentsService(IRepository<Comment> commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

        public AllCommentsByProductViewModel GetAllByProduct(int id)
        {
            var comments = _commentsRepository.All()
                .Where(c => c.ProductId == id)
                .To<CommentViewModel>()
                //.Select(c => new CommentViewModel
                //{
                //    Id = c.Id,
                //    Content = c.Content,
                //    ProductId = c.ProductId,
                //    UserId = c.UserId
                //})
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

            await _commentsRepository.AddAsync(comment);
            await _commentsRepository.SaveChangesAsync();

            var id = comment.Id;/*_commentsRepository.All().Single(c => c.Content.Equals(model.Content)).Id;*/

            return id;
        }

        public async Task<int> Edit(CommentViewModel model)
        {
            var comment = _commentsRepository.All()
                .FirstOrDefault(x => x.Id == model.Id); ;

            if (comment == null)
            {
                throw new KeyNotFoundException();
            }

            comment.Content = model.Content;

            _commentsRepository.Update(comment);
            await _commentsRepository.SaveChangesAsync();

            var id = comment.Id;

            return id;
        }

        public async Task<int> Delete(int id)
        {
            var comment = this._commentsRepository.All()
                .FirstOrDefault(c => c.Id == id);

            if (comment == null)
            {
                throw new KeyNotFoundException();
            }

            _commentsRepository.Delete(comment);

            var index = await _commentsRepository.SaveChangesAsync();

            return index;
        }

        public CommentViewModel GetById(int id)
        {
            var model = _commentsRepository.All()
                .Where(c => c.Id == id)
                .To<CommentViewModel>()
                .FirstOrDefault();

            return model;  
        }
    }
}
