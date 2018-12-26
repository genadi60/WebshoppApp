using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopApp.Services.Models;
using WebshopApp.Services.Models.ViewModels;

namespace WebshopApp.Services.DataServices.Contracts
{
    public interface IProductsService
    {
        IEnumerable<ProductViewModel> GetAll();

        Task<int> Create(int categoryId, string name, string description, decimal price);

        Task<int> Edit(int id, string categoryName, string name, string description, decimal price);

        void Delete(int id);

        TViewModel GetProductById<TViewModel>(int id);

        IEnumerable<ProductViewModel> GetAllByCategory(int categoryId);

        bool AddRatingToProduct(int productId, int rating);
    }
}
