using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebshopApp.Models;
using WebshopApp.Services.MappingServices;

namespace WebshopApp.Services.Models.ViewModels
{
    public class ProductViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public int CategoryId { get; set; }

        public ICollection<Image> Images { get; set; }

        public string ImagePath { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, ProductViewModel>()
                .ForMember(c => c.ImagePath,
                    m => m.MapFrom(p => p.Images.Select(x => x.FileName).FirstOrDefault()));
        }
    }
}
