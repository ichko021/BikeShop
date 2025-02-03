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
        private readonly ILogger<BikeService> _logger;

        public BikeService(IBikeRepository bikeRepository, ILogger<BikeService> logger)
        {
            _bikeRepository = bikeRepository;
            _logger = logger;
        }

        public Bike? AddBike(Bike bike)
        {
            try
            {
                return _bikeRepository.AddBike(bike);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot add bike. {ex.Message} | {ex.StackTrace}");
                throw;
            }
        }

        public void DeleteBikeById(string id)
        {
            
            try
            {
                _bikeRepository.DeleteBikeById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot delete bike by id {id}. {ex.Message} | {ex.StackTrace}");
                throw;
            }
        }

        public List<Bike>? GetAllBikes()
        {
            try
            {
                return _bikeRepository.GetAllBikes();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot fetch bikes. {ex.Message} | {ex.StackTrace}");
                throw;
            }
        }

        public Bike? GetBikeById(string id)
        {
            try
            {
                return _bikeRepository.GetBikeById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot fetch bike by id {id}. {ex.Message} | {ex.StackTrace}");
                throw;
            }
        }

        public Bike? UpdateBikeById(string id, Bike bike)
        {

            var bikeFetchedById = GetBikeById(id);

            if(bikeFetchedById == null)
            {
                return null;
            }

            bikeFetchedById.brand = bike.brand;
            bikeFetchedById.model = bike.model;
            bikeFetchedById.price = bike.price;
            bikeFetchedById.availabilityInStore = bike.availabilityInStore;

            try
            {
                return _bikeRepository.UpdateBikeById(id, bikeFetchedById);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot update bike by id {id}. {ex.Message} | {ex.StackTrace}");
                throw;
            }
        }
    }
}
