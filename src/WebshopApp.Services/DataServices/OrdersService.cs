using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebshopApp.Data.Common;
using WebshopApp.Models;
using WebshopApp.Services.DataServices.Contracts;

namespace WebshopApp.Services.DataServices
{
    public class OrdersService : IOrdersService
    {
        private readonly IRepository<Order> _orderRepository;

        private readonly IRepository<Product> _productRepository;

        public OrdersService(IRepository<Order> orderRepository, IRepository<Product> productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public string Create(int productId, int quantity, string userId = null)
        {
            var order = new Order
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = productId,
                Quantity = quantity,
                ClientId = userId
            };

            _orderRepository.AddAsync(order);
            _orderRepository.SaveChangesAsync();

            var product = _productRepository.All()
                .FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                product.Unit -= quantity;
            }
            
            _productRepository.Update(product);

            return order.Id;
        }
    }
}
