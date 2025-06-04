using BikeShop.DTO.DTO;

namespace BikeShop.BL.Interfaces
{
    public interface IBikeService
    {
        Task<List<Bike>>? GetAllBikes();
        Task<Bike?> GetBikeById(string id);
        Task DeleteBikeById(string id);
        Task<Bike?> AddBike(Bike bike);
        Task<Bike?> UpdateBikeById(string id, Bike bike);
    }
}
