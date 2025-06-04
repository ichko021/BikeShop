using Confluent.Kafka;
using MessagePack;

namespace BikeShop.DTO.Serialization
{
    public class MessagePackSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            return MessagePackSerializer.Serialize(data);
        }
    }
}
