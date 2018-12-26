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
    public class ProductsService : IProductsService
    {
        private readonly IRepository<Product> productsRepository;
        private readonly IRepository<Category> _categoryRepository;

        public ProductsService(IRepository<Product> productsRepository, IImagesService imagesService, IRepository<Category> categoryRepository)
        {
            this.productsRepository = productsRepository;
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<ProductViewModel> GetAll() => this.productsRepository.All().To<ProductViewModel>();
        
        public async Task<int> Create(int categoryId, string name, string description, decimal price)
        {
            var product = new Product()
            {
                CategoryId = categoryId,
                Name = name,
                Description = description,
                Price = price
            };

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();

            return product.Id;
        }

        public async Task<int> Edit(int id, string categoryName, string name, string description, decimal price)
        {
            var product = productsRepository.All()
                .FirstOrDefault(p => p.Id == id);

            var category = _categoryRepository.All()
                .FirstOrDefault(c => c.Name.Equals(categoryName));

            if (product == null)
            {
                throw new KeyNotFoundException();
            }

            //product.Id = id;
            product.Category = category;
            product.Name = name;
            product.Description = description;
            product.Price = price;

            await this.productsRepository.SaveChangesAsync();

            return product.Id;
        }

        public void Delete(int id)
        {
            var product = productsRepository.All().FirstOrDefault(x => x.Id == id);

            this.productsRepository.Delete(product);
            this.productsRepository.SaveChangesAsync();
        }

        public TViewModel GetProductById<TViewModel>(int id)
        {
            var product = this.productsRepository.All().Where(x => x.Id == id)
                .To<TViewModel>().FirstOrDefault();

            return product;
        }

        public IEnumerable<ProductViewModel> GetAllByCategory(int categoryId) 
            => this.productsRepository.All()
                .Where(p => p.CategoryId == categoryId)
                .To<ProductViewModel>();

        public bool AddRatingToProduct(int productId, int rating)
        {
            throw new System.NotImplementedException();
        }
    }
}
