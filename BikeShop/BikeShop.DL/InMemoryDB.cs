using BikeShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeShop.DL
{
    internal static class InMemoryDB
    {
        internal static List<Bike> listOfBikes = new List<Bike>
        {
            new Bike
            {
                brand = "Drag",
                model = "Trigger",
                price = 1000.00,
                availabilityInStore = 10
            },
            new Bike
            {
                brand = "Cube",
                model = "Reaction",
                price = 2000.00,
                availabilityInStore = 5
            },
            new Bike
            {
                brand = "Trek",
                model = "Marlin",
                price = 1700.00,
                availabilityInStore = 4
            }
        };
    }
}
