using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopApp.Services.Models;
using WebshopApp.Services.Models.ViewModels;

namespace WebshopApp.Services.DataServices.Contracts
{
    public interface ICategoriesService
    {
        IEnumerable<CategoryViewModel> GetAll();

        Task<int> Create(string name);

        bool IsCategoryIdValid(int categoryId);

        int GetCategoryId(string name);

        IEnumerable<ProductViewModel> GetAllProductsFromCategory(int categoryId);
    }
}
