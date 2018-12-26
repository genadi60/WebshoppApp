using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Moq;
using WebshopApp.Data;
using WebshopApp.Data.Common;
using WebshopApp.Models;
using WebshopApp.Services.DataServices.Contracts;
using WebshopApp.Services.MappingServices;
using WebshopApp.Services.Models;
using WebshopApp.Services.Models.ViewModels;
using Xunit;

namespace WebshopApp.Services.DataServices.Tests
{
    public class ProductsServiceTests : FakeServices
    {
        public ProductsServiceTests(IServiceProvider serviceProvider, WebshopAppContext context) 
            : base(serviceProvider, context)
        {
        }

        public List<ProductViewModel> GetTestData()
        {
            return  new List<ProductViewModel>
            {
                new ProductViewModel
                {
                    Name = "1",
                    Description = "12345",
                    Price = 1.11m,
                },

                new ProductViewModel
                {
                    Name = "2",
                    Description = "567890",
                    Price = 2.2m
                }
            };
        }

        //[Fact]
        //public void Method_ReturnsCorrect_WhenCorrectIsGiven()
        //{

        //    var repo = new Mock<IRepository<ProductViewModel>>();
        //    repo.Setup(r => r.All()).Returns(GetTestData().AsQueryable);

        //    IProductsService service = new ProductsService(repo.Object, null);
        //    var products = service.GetAll();
        //}
    }
}
