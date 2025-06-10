using BikeShop.DL.Cache;
using BikeShop.DL.Interfaces;
using BikeShop.DL.Kafka;
using BikeShop.DL.Kafka.KafkaCache;
using BikeShop.DL.Repositories;
using BikeShop.DL.Repositories.MongoDB;
using BikeShop.DTO.Configurations;
using BikeShop.DTO.POCO;
using BikeStore.DL.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BikeShop.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection
            AddDataDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IBikeRepository, BikeRepository>();
            services.AddSingleton<IPartRepository, PartRepository>();
            services.AddSingleton<IShopLocationGateway, ShopLocationGateway>();

            services.AddCache<BikeCacheConfiguration, BikeRepository, Bike, string>(config);

            services.AddHostedService<KafkaCache<string, Bike>>();

            return services;
        }

  
        public static IServiceCollection AddCache<TCacheConfiguration, TCacheRepository, TData, TKey>(this IServiceCollection services, IConfiguration config)
           where TCacheConfiguration : CacheConfiguration
           where TCacheRepository : class, ICacheRepository<TKey, TData>
           where TData : ICacheItem<TKey>
           where TKey : notnull
        {
            var configSection = config.GetSection(typeof(TCacheConfiguration).Name);

            if (!configSection.Exists())
            {
                throw new ArgumentNullException(typeof(TCacheConfiguration).Name, "Configuration section is missing in appsettings!");
            }

            services.Configure<TCacheConfiguration>(configSection);

            services.AddSingleton<ICacheRepository<TKey, TData>, TCacheRepository>();
            services.AddSingleton<IKafkaProducer<TData>, KafkaProducer<TKey, TData, TCacheConfiguration>>();
            services.AddHostedService<MongoCachePopulator<TData, TCacheConfiguration, TKey>>();

            return services;
        }
    }
}
