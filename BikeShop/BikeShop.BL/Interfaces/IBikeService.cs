using BikeShop.DTO.DTO;

namespace BikeShop.BL.Interfaces
{
    public interface IBikeService
    {
        List<Bike>? GetAllBikes();
        Bike? GetBikeById(string id);
        void DeleteBikeById(string id);
        Bike? AddBike(Bike bike);
        Bike? UpdateBikeById(string id, Bike bike);
    }
}
