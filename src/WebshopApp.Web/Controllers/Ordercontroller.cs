using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebshopApp.Models;
using WebshopApp.Services.DataServices.Contracts;

namespace WebshopApp.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrdersService _ordersService;

        private readonly SignInManager<WebShopUser> _signInManager;

        private readonly UserManager<WebShopUser> _userManager;

        public OrderController(IOrdersService ordersService, SignInManager<WebShopUser> signInManager, UserManager<WebShopUser> userManager)
        {
            _ordersService = ordersService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Create(int id, int quantity)
        {
            bool isAuthenticated = _signInManager.IsSignedIn(User);

            string userId = null;
            string orderId = null;
            Task<string> result = null;

            if (isAuthenticated)
            {
                userId = _userManager.GetUserId(User);
            
                result = _ordersService.Create(id, quantity, userId);

                orderId = result.Result;

                var user = _userManager.Users.FirstOrDefault(u => u.UserName.Equals(User.Identity.Name));

                var cartId = user.CartId;

                return cartId != null
                    ? RedirectToAction("AddToCart", "Cart", new {cartId, orderId})
                    : RedirectToAction("Create", "Cart", new {userId, orderId});
            }

            result = _ordersService.Create(id, quantity, null);

            orderId = result.Result;

            return RedirectToAction("Index", "Cart", new {orderId = orderId});
        }
    }
}
