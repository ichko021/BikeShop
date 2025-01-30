using BikeShop.BL.Interfaces;
using BikeShop.DL.Interfaces;
using BikeShop.DL.Repositories;
using BikeShop.DTO.DTO;
using Microsoft.Extensions.Logging;
using Serilog.Core;

namespace BikeShop.BL.Services
{
    public class BikeService : IBikeService
    {
        private readonly IBikeRepository _bikeRepository;

        public BikeService(IBikeRepository bikeRepository)
        {
            _bikeRepository = bikeRepository;
        }

        public Bike? AddBike(Bike bike)
        {
            try
            {
                return _bikeRepository.AddBike(bike);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteBikeById(string id)
        {
            _bikeRepository.DeleteBikeById(id);
        }

        public List<Bike>? GetAllBikes()
        {
            return _bikeRepository.GetAllBikes();
        }

        public Bike? GetBikeById(string id)
        {
            return _bikeRepository.GetBikeById(id);
        }

        public Bike? UpdateBikeById(string id, Bike bike)
        {
            var bikeFetchedById = _bikeRepository.GetBikeById(id);

            if(bikeFetchedById == null)
            {
                return null;
            }

            bikeFetchedById.brand = bike.brand;
            bikeFetchedById.model = bike.model;
            bikeFetchedById.price = bike.price;
            bikeFetchedById.availabilityInStore = bike.availabilityInStore;

            return _bikeRepository.UpdateBikeById(id, bikeFetchedById);
        }
    }
}
