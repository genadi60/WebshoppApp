using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebshopApp.Data;
using WebshopApp.Data.Common;
using WebshopApp.Models;
using WebshopApp.Services.DataServices.Contracts;
using WebshopApp.Services.Models.ViewModels;

namespace WebshopApp.Services.DataServices
{
    public class OrdersService : IOrdersService
    {
        private readonly IRepository<Order> _orderRepository;

        private readonly IRepository<Product> _productRepository;

        private readonly WebshopAppContext _context;

        public OrdersService(IRepository<Order> orderRepository, IRepository<Product> productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<string> Create(CartViewModel model, string userId = null)
        {
            var order = new Order
            {
                Id = Guid.NewGuid().ToString(),
                Products = model.Products,
                ClientId = userId
            };

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            foreach (var product in model.Products)
            {
                product.Unit -= product.Quantity;
            }

            _productRepository.UpdateRange(model.Products);
            await _productRepository.SaveChangesAsync();

            return order.Id;
        }
    }
}
