using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using WebshopApp.Models;
using WebshopApp.Services.Models.ViewModels;

namespace WebshopApp.Services.DataServices.Contracts
{
    public interface ICartsService
    {
        CartViewModel GetShoppingCart(HttpContext context);
    }
}
