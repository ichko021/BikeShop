using BikeShop.DTO;

namespace BikeShop.BL.Interfaces
{
    public interface IBikeService
    {
        List<Bike> GetAllBikes();
        Bike? GetBikeById(int id);
        void DeleteBikeById(int id);
        void AddBike(Bike bike);
        void UpdateBikeById(int id, Bike bike);
    }
}
