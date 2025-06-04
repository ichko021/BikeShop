using BikeShop.DTO.Configurations;
using BikeShop.DTO.POCO;
using BikeStore.DL.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace BikeShop.DL.Cache
{
    public class MongoCachePopulator<TData, TConfigurationType, TKey> : BackgroundService
        //where TDataRepository : ICacheRepository<TKey, TData>
        where TKey : notnull
        where TData : ICacheItem<TKey>
        where TConfigurationType : CacheConfiguration
    {
        private readonly ICacheRepository<TKey, TData> _cacheRepository;
        private readonly IOptionsMonitor<TConfigurationType> _configuration;
        private readonly IKafkaProducer<TData> _kafkaProducer;

        public MongoCachePopulator(ICacheRepository<TKey, TData> cacheRepository, IOptionsMonitor<TConfigurationType> configuration, IKafkaProducer<TData> kafkaProducer)
        {
            _cacheRepository = cacheRepository;
            _configuration = configuration;
            _kafkaProducer = kafkaProducer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var lastExecuted = DateTime.UtcNow;

            try
            {
                var result = await _cacheRepository.FullLoad();

                if (result != null && result.Any())
                {
                    await _kafkaProducer.ProduceAll(result);
                }
            }
            catch (Exception ex)
            {
                // Log but don't crash the service
                Console.WriteLine($"[CachePopulator] Initial load failed: {ex.Message}");
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(
                        TimeSpan.FromSeconds(_configuration.CurrentValue.RefreshInterval),
                        stoppingToken);

                    var updatedData = await _cacheRepository.DifLoad(lastExecuted);

                    if (updatedData != null && updatedData.Any())
                    {
                        await _kafkaProducer.ProduceAll(updatedData);
                        lastExecuted = updatedData.Last()?.DateInserted ?? DateTime.UtcNow;
                    }
                }
                catch (TaskCanceledException)
                {
                    // Graceful shutdown
                    break;
                }
                catch (Exception ex)
                {
                    // Log and keep looping
                    Console.WriteLine($"[CachePopulator] Error during loop: {ex.Message}");
                }
            }
        }
    }
}
