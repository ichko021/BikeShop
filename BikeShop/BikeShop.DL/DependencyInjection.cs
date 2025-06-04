using BikeShop.DL.Interfaces;
using BikeShop.DL.Repositories;
using BikeShop.DL.Kafka;
using Microsoft.Extensions.DependencyInjection;

namespace BikeShop.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            return services
                .AddSingleton<IBikeRepository, BikeRepository>()
                .AddSingleton<IPartRepository, PartRepository>()
                .AddSingleton<KafkaProducer>()
                .AddHostedService<KafkaConsumer>();

        }
    }
}
