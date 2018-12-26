﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebshopApp.Services.DataServices.Contracts;
using WebshopApp.Services.Models.InputModels;
using WebshopApp.Services.Models.ViewModels;

namespace WebshopApp.Web.Controllers
{
    public class BlogController : BaseController
    {
        private readonly IBlogsService blogsService;
        private readonly ICommentsService commentsService;

        public BlogController(IBlogsService blogsService, ICommentsService commentsService)
        {
            this.blogsService = blogsService;
            this.commentsService = commentsService;
        }

        public IActionResult Index()
        {
            var blogs = this.blogsService.GetAll().ToList();

            var viewModels = new List<BlogViewModel>();

            foreach (var blog in blogs)
            {
                viewModels.Add(blog);
            }

            return this.View(viewModels);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IActionResult> Create(CreateBlogInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var id = await this.blogsService.Create(model.Title, model.Content);

            return this.RedirectToAction("Details", "Blog", new { id = id });
        }

        public IActionResult Details(int id)
        {
            var blog = this.blogsService.GetBlogById<BlogViewModel>(id);

            return this.View(blog);
        }

        //public IActionResult Comments(int? id)
        //{
        //    var comments = commentsService.GetAll().Where(x => x.ProductId == id).ToArray();
        //    return Json(comments, JsonRequestBehavior.AllowGet);
        //}
    }
}
