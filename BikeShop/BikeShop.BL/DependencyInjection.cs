using Microsoft.Extensions.DependencyInjection;
using BikeShop.BL.Interfaces;
using BikeShop.BL.Services;
using BikeShop.DL;

namespace BikeShop.BL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IBikeService, BikeService>()
                    .AddSingleton<IPartService, PartService>();

            return services;
        }
    }
}
