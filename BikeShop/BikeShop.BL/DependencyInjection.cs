using Microsoft.Extensions.DependencyInjection;
using BikeShop.BL.Interfaces;
using BikeShop.BL.Services;
using BikeShop.DL;

namespace BikeShop.BL
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterBusinessLayer(this IServiceCollection services)
        {
            services.AddSingleton<IBikeService, BikeService>();

            return services;
        }

        public static IServiceCollection RegisterDataLayer(this IServiceCollection services)
        {
            services.RegisterRepositories();

            return services;
        }
    }
}
