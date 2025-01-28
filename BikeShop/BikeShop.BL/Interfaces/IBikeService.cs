using BikeShop.DTO.DTO;

namespace BikeShop.BL.Interfaces
{
    public interface IBikeService
    {
        List<Bike> GetAllBikes();
        Bike? GetBikeById(string id);
        void DeleteBikeById(string id);
        void AddBike(Bike bike);
        void UpdateBikeById(string id, Bike bike);
    }
}
