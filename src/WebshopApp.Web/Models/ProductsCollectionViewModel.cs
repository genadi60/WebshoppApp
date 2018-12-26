using System.Collections.Generic;
using WebshopApp.Services.Models;
using WebshopApp.Services.Models.ViewModels;

namespace WebshopApp.Web.Models
{
    public class ProductsCollectionViewModel
    {
        public virtual ICollection<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
    }
}
