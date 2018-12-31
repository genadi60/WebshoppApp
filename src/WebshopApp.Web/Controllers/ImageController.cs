using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebshopApp.Models;
using WebshopApp.Services.DataServices.Contracts;

namespace WebshopApp.Web.Controllers
{
    public class ImageController : BaseController
    {
        private readonly IImagesService imagesService;

        public ImageController(IImagesService imagesService)
        {
            this.imagesService = imagesService;
        }

        [HttpPost]
        public IActionResult Upload(string productId, List<IFormFile> files)
        {
            this.imagesService.UploadImagesToProduct(productId, files);

            return RedirectToAction("Details", "Product", productId);
        }

        public IEnumerable<Image> GetImagesOfProduct(string productId)
        {
            return this.imagesService.GetImagesOfProduct(productId);
        }
    }
}
