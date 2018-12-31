using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using WebshopApp.Data.Common;
using WebshopApp.Models;
using WebshopApp.Services.DataServices.Contracts;
using WebshopApp.Services.MappingServices;
using WebshopApp.Services.Models.ViewModels;
using SessionExtensions = WebshopApp.Data.Common.SessionExtensions;

namespace WebshopApp.Services.DataServices
{
    public class CartsService : ICartsService
    {
        private IDistributedCache _cache;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IOrdersService _ordersService;
        private readonly IRepository<Product> _productRepository;

        public CartsService(IRepository<Cart> cartRepository, IRepository<Order> orderRepository, IRepository<Product> productRepository, IOrdersService ordersService, IDistributedCache cache)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _ordersService = ordersService;
            _cache = cache;
        }

        public CartViewModel GetShoppingCart(HttpContext context)
        {
            
            if (SessionExtensions.Get<Cart>(context.Session, "Cart") == null)
            {
                var cart = new Cart
                {
                    Id = Guid.NewGuid().ToString()
                };
                
                SessionExtensions.Set(context.Session, "Cart", cart);
                //context.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            }

            dynamic cartObject = SessionExtensions.Get<Cart>(context.Session,"Cart") == null
                ? null
                : JsonConvert.DeserializeObject<Cart>(context.Session.GetString("Cart"));

            string id = cartObject.Id;

            ICollection<Product> products = cartObject.Products;

            var model = new CartViewModel
            {
                Id = id,
                Products = products,
                Total = products.Sum(p => p.Price * p.Quantity)
            };
            
            return model;
        }

       public CartViewModel AddToShoppingCart(HttpContext context, string productId, int quantity)
       {
           Cart cart;
           var id = productId;
            if (SessionExtensions.Get<Cart>(context.Session, "Cart") == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid().ToString()
                };

                SessionExtensions.Set(context.Session, "Cart", cart);
                //context.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            }

            dynamic cartObject = context.Session.GetString("Cart") == null
                ? null
                : JsonConvert.DeserializeObject<Cart>(context.Session.GetString("Cart"));

            string cartId = cartObject.Id;

            ICollection<Product> products = cartObject.Products;

            var product = _productRepository.All()
                .FirstOrDefault(p => p.Id.Equals(id));
            product.Quantity = quantity;
            product.Unit -= quantity;

            _productRepository.Update(product);
            _productRepository.SaveChangesAsync();

            products.Add(product);

            cart = new Cart
            {
                Id = cartId,
                Products = products
            };

            SessionExtensions.Set(context.Session, "Cart", cart);

            var model = new CartViewModel
            {
                Id = cartId,
                Products = products,
                Total = products.Sum(p => p.Price * p.Quantity)
            };

            return model;
        }
    }
}
