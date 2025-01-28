using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeShop.DTO.Requests
{
    public class UpdatePartRequest
    {
        public string id { get; set; }
        public string partName { get; set; }
        public string partSpec { get; set; }
    }
}
