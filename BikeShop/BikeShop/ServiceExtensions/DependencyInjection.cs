using BikeShop.DTO.Configurations;

namespace BikeShop.ServiceExtensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<MongoDbConfig>(
                configuration.GetSection(nameof(MongoDbConfig)));

            return services;
        }
    }
}
