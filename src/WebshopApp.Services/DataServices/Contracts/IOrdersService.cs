using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebshopApp.Services.DataServices.Contracts
{
    public interface IOrdersService
    {
        string Create(int productId, int quantity, string userId);
    }
}
