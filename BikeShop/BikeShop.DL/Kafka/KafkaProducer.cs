using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace BikeShop.DL.Kafka
{
    public class KafkaProducer
    {
        private readonly IProducer<Null, string> _producer;
        private readonly string _topic;

        public KafkaProducer(IOptions<KafkaSettings> kafkaSettings)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = kafkaSettings.Value?.BootstrapServers ?? "localhost:9092"
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
            _topic = kafkaSettings.Value?.Topic ?? "cache-events";
        }

        public async Task ProduceAsync(string message)
        {
            try
            {
                var result = await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
                Console.WriteLine($"Message delivered to: {result.TopicPartitionOffset}");
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                throw;
            }
        }
    }
}