using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebshopApp.Models;
using WebshopApp.Services.Models.ViewModels;

namespace WebshopApp.Services.DataServices.Contracts
{
    public interface ICartsService
    {
        CartViewModel GetShoppingCart(HttpContext context);

        CartViewModel AddToShoppingCart(HttpContext context, string productId, int quantity);
    }
}
