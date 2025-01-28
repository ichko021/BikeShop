using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeShop.DTO.Responses
{
    public class GetBikeResponse
    {
        public string id { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public double price { get; set; }
        public int availabilityInStore { get; set; }
    }
}
