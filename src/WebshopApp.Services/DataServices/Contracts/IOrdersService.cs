using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebshopApp.Services.Models.ViewModels;

namespace WebshopApp.Services.DataServices.Contracts
{
    public interface IOrdersService
    {
        Task<string> Create(CartViewModel model, string userId);
    }
}
