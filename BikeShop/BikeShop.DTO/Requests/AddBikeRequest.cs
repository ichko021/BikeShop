using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeShop.DTO.Requests
{
    public class AddBikeRequest
    {
        public string brand { get; set; }
        public string model { get; set; }
        public double price { get; set; }
        public int availabilityInStore { get; set; }
    }
}
