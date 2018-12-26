using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebshopApp.Models;
using WebshopApp.Services.DataServices.Contracts;
using WebshopApp.Services.Models;
using WebshopApp.Services.Models.InputModels;
using WebshopApp.Services.Models.ViewModels;
using WebshopApp.Web.Models;
using CreateProductInputModel = WebshopApp.Services.Models.InputModels.CreateProductInputModel;

namespace WebshopApp.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;
        private readonly IImagesService imagesService;

        public ProductController(IProductsService productsService, ICategoriesService categoriesService, IImagesService imagesService)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
            this.imagesService = imagesService;
        }

        public IActionResult Details(int id)
        {
            var product = this.productsService.GetProductById<ProductViewModel>(id);

            return this.View(product);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            this.ViewData["Categories"] = this.categoriesService.GetAll()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                });

            var product = this.productsService.GetProductById<EditProductInputModel>(id);

            return this.View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductInputModel model, List<IFormFile> files)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var id = await this.productsService.Edit(model.Id, model.Category.Name, model.Name, model.Description, model.Price);
            this.imagesService.UploadImagesToProduct(model.Id, files);

            return this.RedirectToAction("Details", new { id = model.Id });
        }

        [Authorize]
        public IActionResult Create()
        {
            this.ViewData["Categories"] = this.categoriesService.GetAll()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                });
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductInputModel model, List<IFormFile> files)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var id = await this.productsService.Create(model.CategoryId, model.Name, model.Description, model.Price);
            this.imagesService.UploadImagesToProduct(id, files);

            return this.RedirectToAction("Details", new { id = id });
        }
        
        public IActionResult Delete(int id)
        {
            var product = this.productsService.GetProductById<ProductViewModel>(id);

            return this.View(product);
        }

        [HttpPost]
        public IActionResult Delete(ProductViewModel model)
        {
            this.productsService.Delete(model.Id);

            return this.RedirectToAction("Deleted", "Product");
        }

        public IActionResult Deleted()
        {
            return this.View();
        }
    }
}
