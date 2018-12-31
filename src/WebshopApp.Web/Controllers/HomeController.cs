using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebshopApp.Services.DataServices;
using WebshopApp.Services.DataServices.Contracts;
using WebshopApp.Services.Models;
using WebshopApp.Services.Models.ViewModels;
using WebshopApp.Web.Models;
using X.PagedList;

namespace WebshopApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public IActionResult Index(int? page)
        {
            var products = this.productsService.GetAll<ProductViewModel>().ToList();

            var viewModels = new List<ProductViewModel>();

            foreach (var product in products)
            {
                viewModels.Add(product);
            }

            var nextPage = page ?? 1;

            //var pagedViewModels = viewModels.ToPagedList(nextPage, 3);

            return View(viewModels);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
