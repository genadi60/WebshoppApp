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
        CartViewModel GetShoppingCart(HttpContext context, string orderId = null);

        CartViewModel GetShoppingCart(string cartId);

        CartViewModel Create(string clientId, string orderId);

        Task<int> AddToShoppingCart(string cartId, string orderId);
    }
}
