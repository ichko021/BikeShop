using BikeShop.DTO.DTO;

namespace BikeShop.DL.Interfaces
{
    public interface IBikeRepository
    {
        List<Bike> GetAllBikes();
        Bike? GetBikeById(string id);
        void DeleteBikeById(string id);
        void AddBike(Bike bike);
        void UpdateBikeById(string id, Bike bike);
    }
}
