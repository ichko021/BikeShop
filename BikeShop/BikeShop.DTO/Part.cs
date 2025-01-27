using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BikeShop.DTO
{
    public class Part
    {
        public string partName { get; set; }
        public string partSpec { get; set; }
    }
}
