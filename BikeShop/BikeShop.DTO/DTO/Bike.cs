using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BikeShop.DTO.DTO
{
    public class Bike
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public double price { get; set; }
        public int availabilityInStore { get; set; }
    }
}
