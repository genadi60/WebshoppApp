using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopApp.Services.Models;
using WebshopApp.Services.Models.ViewModels;

namespace WebshopApp.Services.DataServices.Contracts
{
    public interface IBlogsService
    {
        IEnumerable<BlogViewModel> GetAll();

        Task<int> Create(string title, string content);

        TViewModel GetBlogById<TViewModel>(int id);

        //bool IsCategoryIdValid(int categoryId);

        //int GetCategoryId(string name);

        //IEnumerable<BlogViewModel> GetAllPostsFromCategory(int categoryId);
    }
}
