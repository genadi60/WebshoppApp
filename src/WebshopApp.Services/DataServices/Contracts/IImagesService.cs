using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using WebshopApp.Models;

namespace WebshopApp.Services.DataServices.Contracts
{
    public interface IImagesService
    {
        void UploadImagesToProduct(string productId, List<IFormFile> files);

        IEnumerable<Image> GetImagesOfProduct(string productId);
    }
}
