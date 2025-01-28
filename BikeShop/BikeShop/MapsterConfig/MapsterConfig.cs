using BikeShop.DTO.DTO;
using BikeShop.DTO.Requests;
using Mapster;


namespace BikeShop.MapsterConfig
{
    public static class MapsterConfig
    {
        public static void Configure()
        {
            TypeAdapterConfig<Bike, AddBikeRequest>
                .NewConfig()
                .TwoWays();
            TypeAdapterConfig<Part, AddPartRequest>
                .NewConfig()
                .TwoWays();
        }
    }
}
