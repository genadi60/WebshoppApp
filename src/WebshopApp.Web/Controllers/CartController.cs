using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebshopApp.Data;
using WebshopApp.Models;
using WebshopApp.Services.DataServices.Contracts;
using WebshopApp.Services.Models.InputModels;
using WebshopApp.Services.Models.ViewModels;

namespace WebshopApp.Web.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartsService _cartsService;

        private readonly HttpContext _context;

        public CartController(ICartsService cartsService, HttpContext context)
        {
            _cartsService = cartsService;
            _context = context;
        }


        public IActionResult Index()
        {
            var cartModel = _cartsService.GetShoppingCart(_context);

            return View(cartModel);
        }

        [HttpPost]
        public IActionResult AddToCart(string productId, int quantity)
        {

            var cartModel = _cartsService.AddToShoppingCart(_context, productId, quantity);

            return View("Index", cartModel);
        }

    }
}
