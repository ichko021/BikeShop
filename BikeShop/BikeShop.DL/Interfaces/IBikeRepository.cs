using BikeShop.DTO;

namespace BikeShop.DL.Interfaces
{
    public interface IBikeRepository
    {
        List<Bike> GetAllBikes();
        Bike? GetBikeById(int id);
        void DeleteBikeById(int id);
        void AddBike(Bike bike);
        void UpdateBikeById(int id, Bike bike);
    }
}
