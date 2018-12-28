using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebshopApp.Data;
using WebshopApp.Models;
using WebshopApp.Services.DataServices.Contracts;

namespace WebshopApp.Web.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartsService _cartsService;
        private readonly WebshopAppContext _context;

        public CartController(ICartsService cartsService, WebshopAppContext context)
        {
            _cartsService = cartsService;
            _context = context;
        }

        public IActionResult Index()
        {
            var cartModel = _cartsService.GetShoppingCart(HttpContext);

            return View(cartModel);
        }
    }
}
