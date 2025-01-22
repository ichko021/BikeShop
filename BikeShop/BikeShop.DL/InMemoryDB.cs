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
                id = 1,
                brand = "Drag",
                model = "Trigger",
                price = 1000.00,
                availabilityInStore = 10,
                bikeParts = new Parts
                {
                    wheelSize = 29,
                    speeds = "2x11",
                    fork = "Rockshox"
                }
            },
            new Bike
            {
                id = 1,
                brand = "Cube",
                model = "Reaction",
                price = 2000.00,
                availabilityInStore = 5,
                bikeParts = new Parts
                {
                    wheelSize = 29,
                    speeds = "1x10",
                    fork = "SR Suntour"
                }
            },
            new Bike
            {
                id = 1,
                brand = "Trek",
                model = "Marlin",
                price = 1700.00,
                availabilityInStore = 4,
                bikeParts = new Parts
                {
                    wheelSize = 29,
                    speeds = "1x10",
                    fork = "Rockshox"
                }
            }
        };
    }
}
