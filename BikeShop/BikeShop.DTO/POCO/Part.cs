using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MessagePack;

namespace BikeShop.DTO.POCO
{
    [MessagePackObject]
    public class Part : ICacheItem<string>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [Key(0)]
        public string id { get; set; }
        [Key(1)]
        public string partName { get; set; }
        [Key(2)]
        public string partSpec { get; set; }
        [Key(3)]
        public DateTime DateInserted { get; set; }

        public string GetKey()
        {
            return id;
        }
    }
}
