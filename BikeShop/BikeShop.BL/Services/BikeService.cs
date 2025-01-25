using BikeShop.BL.Interfaces;
using BikeShop.DL.Interfaces;
using BikeShop.DTO;

namespace BikeShop.BL.Services
{
    internal class BikeService : IBikeService
    {
        private IBikeRepository _bikeRepository;

        public BikeService(IBikeRepository bikeRepository)
        {
            _bikeRepository = bikeRepository;
        }

        public void AddBike(Bike bike)
        {
            _bikeRepository.AddBike(bike);
        }

        public void DeleteBikeById(int id)
        {
            _bikeRepository.DeleteBikeById(id);
        }

        public List<Bike> GetAllBikes()
        {
            return _bikeRepository.GetAllBikes();
        }

        public Bike? GetBikeById(int id)
        {
            return _bikeRepository.GetBikeById(id);
        }

        public void UpdateBikeById(int id, Bike bike)
        {
            _bikeRepository.UpdateBikeById(id, bike);
        }
    }
}
