using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebshopApp.Data;
using WebshopApp.Models;
using WebshopApp.Services.DataServices.Contracts;
using WebshopApp.Services.Models.ViewModels;

namespace WebshopApp.Web.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartsService _cartsService;

        private readonly SignInManager<WebShopUser> _signInManager;

        private readonly UserManager<WebShopUser> _userManager;
        
        public CartController(ICartsService cartsService, SignInManager<WebShopUser> signInManager, UserManager<WebShopUser> userManager)
        {
            _cartsService = cartsService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index(string orderId = null)
        {
            CartViewModel cartModel = null;

            if (_signInManager.IsSignedIn(User))
            {
                var user = _userManager.Users
                    .FirstOrDefault(u => u.UserName.Equals(User.Identity.Name));
                if (user.CartId == null)
                {
                    return RedirectToAction("Create", new {user.Id});
                }

                cartModel = _cartsService.GetShoppingCart(user.CartId);
            }
            else
            {
                cartModel = _cartsService.GetShoppingCart(HttpContext, orderId);
            }
            
            return View(cartModel);
        }

        public IActionResult Create(string userId, string orderId)
        {
            var cartModel = _cartsService.Create(userId, orderId);

            return View("/views/cart/index.cshtml", cartModel);
        }

        [HttpPost]
        public IActionResult AddToCart(string cartId, string orderId)
        {
            _cartsService.AddToShoppingCart(cartId, orderId);
          
            return RedirectToAction("Index");
        }
    }
}
