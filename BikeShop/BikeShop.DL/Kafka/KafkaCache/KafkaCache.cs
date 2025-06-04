using BikeShop.DTO.Serialization;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace BikeShop.DL.Kafka.KafkaCache
{
    public class KafkaCache<TKey, TValue> : BackgroundService//, IKafkaCache<TKey, TValue> where TKey : notnull where TValue : class
    {
        private readonly ConsumerConfig _config;

        public KafkaCache()
        {
            _config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "default-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var consumer = new ConsumerBuilder<TKey, TValue>(_config)
                .SetValueDeserializer(new MessagePackDeserializer<TValue>())
                .Build();

            consumer.Subscribe("cache-events");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = consumer.Consume(TimeSpan.FromMilliseconds(500));

                    if (result == null || result.IsPartitionEOF)
                        continue;

                    // Process message
                    Console.WriteLine($"Consumed: {result.Message.Key}");
                }
                catch (ConsumeException ex)
                {
                    Console.WriteLine($"Kafka consume error: {ex.Error.Reason}");
                    await Task.Delay(1000, stoppingToken); // optional backoff
                }
            }

            consumer.Close(); // clean shutdown
        }

    }
}