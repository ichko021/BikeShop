using BikeShop.DTO.Configurations;

namespace BikeShop.ServiceExtensions
{
    public static class ServiceConfigurationsExtensions
    {
        public static IServiceCollection AddConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)  
        {
            return services.Configure<MongoDbConfig>(
                configuration.GetSection(nameof(MongoDbConfig)));
        }
    }
}
