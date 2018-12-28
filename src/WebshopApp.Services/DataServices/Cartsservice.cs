using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebshopApp.Data.Common;
using WebshopApp.Models;
using WebshopApp.Services.DataServices.Contracts;
using WebshopApp.Services.Models.ViewModels;

namespace WebshopApp.Services.DataServices
{
    public class CartsService : ICartsService
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Order> _orderRepository;

        public CartsService(IRepository<Cart> cartRepository, IRepository<Order> orderRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
        }

        public CartViewModel GetShoppingCart(HttpContext context)
        {
            if (context.Session.Get("Cart") == null)
            {
                var cart = new Cart
                {
                    Id = Guid.NewGuid().ToString(),
                    Orders = _orderRepository.All().ToList()
                };
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
    }
}
