using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace BikeShop.DL.Kafka
{
    public class KafkaConsumer : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly string _topic;

        public KafkaConsumer(IOptions<KafkaSettings> kafkaSettings)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = kafkaSettings.Value?.BootstrapServers ?? "localhost:9092",
                GroupId = kafkaSettings.Value?.GroupId ?? "default-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            _topic = kafkaSettings.Value?.Topic ?? "cache-events";
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe(_topic);

            return Task.Run(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = _consumer.Consume(stoppingToken);
                        if (result != null)
                        {
                            Console.WriteLine($"Received message: {result.Message.Value}");

                            // TODO: Parse message and update cache here

                            _consumer.Commit(result);
                        }
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Consume error: {e.Error.Reason}");
                    }
                }
            }, stoppingToken);
        }

        public override void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();
            base.Dispose();
        }
    }
}