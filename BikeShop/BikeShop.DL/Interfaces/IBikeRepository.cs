using BikeShop.DTO.POCO;
using BikeShop.DL.Cache;

namespace BikeShop.DL.Interfaces
{
    public interface IBikeRepository : ICacheRepository<string, Bike>
    {
        Task<List<Bike>> GetAllBikes();
        Task<Bike?> GetBikeById(string id);
        Task DeleteBikeById(string id);
        Task<Bike?> AddBike(Bike bike);
        Task<Bike?> UpdateBikeById(string id, Bike bike);
    }
}
