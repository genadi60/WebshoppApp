using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebshopApp.Data.Common;
using WebshopApp.Models;
using WebshopApp.Services.DataServices.Contracts;
using WebshopApp.Services.MappingServices;
using WebshopApp.Services.Models.ViewModels;

namespace WebshopApp.Services.DataServices
{
    public class CartsService : ICartsService
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IOrdersService _ordersService;
        private readonly IRepository<Product> _productRepository;

        public CartsService(IRepository<Cart> cartRepository, IRepository<Order> orderRepository, IRepository<Product> productRepository, IOrdersService ordersService)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _ordersService = ordersService;
        }

        public CartViewModel GetShoppingCart(HttpContext context, string orderId = null)
        {
            if (context.Session.Get("Cart") == null)
            {
                var cart = new Cart
                {
                    Id = Guid.NewGuid().ToString()
                };

                if (orderId != null)
                {
                    var order = _orderRepository.All()
                        .FirstOrDefault(o => o.Id.Equals(orderId));
                    cart.Add(order);
                }

                
                context.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            }

            dynamic cartObject = context.Session.GetString("Cart") == null
                ? null
                : JsonConvert.DeserializeObject(context.Session.GetString("Cart"));

            string id = cartObject.Id;

            IEnumerable<Order> orders = cartObject.Orders.ToObject<HashSet<Order>>();

            var model = new CartViewModel
            {
                Id = id,
                Orders = orders
            };
            return model;
        }

        public CartViewModel GetShoppingCart(string cartId)
        {
            var model = _cartRepository.All()
                .Where(c => c.Id.Equals(cartId))
                .Select(c => new CartViewModel
                {
                    Id = c.Id,
                    ClientId = c.ClientId,
                    Orders = c.Orders
                })
                .FirstOrDefault();

            return model;
        }

        [Authorize]
        public CartViewModel Create(string clientId, string orderId)
        {
            var order = _orderRepository.All()
                .FirstOrDefault(o => o.Id == orderId);
                
            var cart = new Cart
            {
                Id = Guid.NewGuid().ToString(),
                ClientId = clientId,
            };
            
            cart.Add(order);

            _cartRepository.AddAsync(cart);
            _cartRepository.SaveChangesAsync();

            var model = new CartViewModel
            {
                Id = cart.Id,
                ClientId = cart.ClientId,
            };

            return model;
        }

        public async Task<int> AddToShoppingCart(string cartId, string orderId)
        {
            var order = _orderRepository.All()
                .FirstOrDefault(o => o.Id.Equals(orderId));

            var cart = _cartRepository.All()
                .FirstOrDefault(c => c.Id.Equals(cartId));

            if (cart != null)
            {
                cart.Add(order);
            }
            

            var result = await _cartRepository.Update(cart);

            return result;
        }
    }
}
