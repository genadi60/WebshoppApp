using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopApp.Services.Models;
using WebshopApp.Services.Models.ViewModels;

namespace WebshopApp.Services.DataServices.Contracts
{
    public interface IProductsService
    {
        IEnumerable<TEntityViewModel> GetAll<TEntityViewModel>();

        Task<string> Create(int categoryId, string name, string description, decimal price);

        Task<string> Edit(string id, string categoryName, string name, string description, decimal price);

        Task Delete(string id);

        TEntityViewModel GetProductById<TEntityViewModel>(string id);

        IEnumerable<TEntityViewModel> GetAllByCategory<TEntityViewModel>(int categoryId);

        bool AddRatingToProduct(string productId, int rating);
    }
}
