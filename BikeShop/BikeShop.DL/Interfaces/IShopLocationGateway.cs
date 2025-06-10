using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeShop.DL.Interfaces
{
    public interface IShopLocationGateway
    {
        Task<string> GetAllLocations();
    }
}
