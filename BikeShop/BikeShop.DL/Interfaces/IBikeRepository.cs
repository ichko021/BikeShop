using BikeShop.DTO.DTO;

namespace BikeShop.DL.Interfaces
{
    public interface IBikeRepository
    {
        Task<List<Bike>> GetAllBikes();
        Task<Bike?> GetBikeById(string id);
        Task DeleteBikeById(string id);
        Task<Bike?> AddBike(Bike bike);
        Task<Bike?> UpdateBikeById(string id, Bike bike);
    }
}
