using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebshopApp.Services.DataServices.Contracts;
using WebshopApp.Services.Models;
using WebshopApp.Services.Models.ViewModels;

namespace WebshopApp.Web.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public CategoryController(ICategoriesService categoriesService, IProductsService productsService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult ListAllProductFromCategory(int id)
        {
            this.ViewData["Categories"] = this.categoriesService.GetAll()
                .Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();

            if (ViewData["Categories"] == null)
            {
                throw new ArgumentException("Categories are empty");
            }

            var products = this.categoriesService.GetAllProductsFromCategory(id).ToList();

            return this.View(products);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            await this.categoriesService.Create(name);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
