using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BikeShop.DTO.DTO
{
    public class Part
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string partName { get; set; }
        public string partSpec { get; set; }
    }
}
