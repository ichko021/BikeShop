using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MessagePack;

namespace BikeShop.DTO.POCO
{
    [MessagePackObject]
    public class Bike : ICacheItem<string>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [Key(0)]
        public string id { get; set; }
        [Key(1)]
        public string brand { get; set; }
        [Key(2)]
        public string model { get; set; }
        [Key(3)]
        public double price { get; set; }
        [Key(4)]
        public int availabilityInStore { get; set; }
        [Key(5)]
        public DateTime DateInserted { get; set; }

        public string GetKey()
        {
            return id;
        }
    }
}
