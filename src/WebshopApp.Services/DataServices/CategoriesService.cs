using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopApp.Data.Common;
using WebshopApp.Models;
using WebshopApp.Services.DataServices.Contracts;
using WebshopApp.Services.MappingServices;
using WebshopApp.Services.Models;
using WebshopApp.Services.Models.ViewModels;

namespace WebshopApp.Services.DataServices
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categoriesRepository;
        private readonly IProductsService productsService;

        public CategoriesService(IRepository<Category> categoriesRepository, IProductsService productsService)
        {
            this.categoriesRepository = categoriesRepository;
            this.productsService = productsService;
        }

        public IEnumerable<CategoryViewModel> GetAll()
        {
            var categories = this.categoriesRepository.All().OrderBy(x => x.Name)
                .To<CategoryViewModel>().ToList();

            return categories;
        }
        
        public async Task<int> Create(string name)
        {
            var category = new Category
            {
                Name = name
            };

            await this.categoriesRepository.AddAsync(category);
            await this.categoriesRepository.SaveChangesAsync();

            return category.Id;
        }

        public bool IsCategoryIdValid(int categoryId) => this.categoriesRepository.All().Any(x => x.Id == categoryId);

        public int GetCategoryId(string name)
        {
            var category = this.categoriesRepository.All().FirstOrDefault(x => x.Name == name);
            if (category == null)
            {
                throw new KeyNotFoundException();
            }
            return category.Id;
        }

        public IEnumerable<ProductViewModel> GetAllProductsFromCategory(int categoryId)
        {
            if (!IsCategoryIdValid(categoryId))
            {
                throw new KeyNotFoundException();
            }

            return this.productsService.GetAllByCategory<ProductViewModel>(categoryId);
        }
    }
}
