using BikeShop.BL.Interfaces;
using BikeShop.DL.Interfaces;
using BikeShop.DTO.DTO;
using Microsoft.Extensions.Logging;

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

        public async Task<Bike?> AddBike(Bike bike)
        {
            try
            {
                return await _bikeRepository.AddBike(bike);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot add bike. {ex.Message}");
                throw;
            }
        }

        public async Task DeleteBikeById(string id)
        {
            try
            {
                await _bikeRepository.DeleteBikeById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot delete bike by id {id}. {ex.Message}");
                throw;
            }
        }

        public async Task<List<Bike>?> GetAllBikes()
        {
            try
            {
                return await _bikeRepository.GetAllBikes();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot fetch bikes. {ex.Message}");
                throw;
            }
        }

        public async Task<Bike?> GetBikeById(string id)
        {
            try
            {
                return await _bikeRepository.GetBikeById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot fetch bike by id {id}. {ex.Message}");
                throw;
            }
        }

        public async Task<Bike?> UpdateBikeById(string id, Bike bike)
        {
            var bikeFetchedById = await GetBikeById(id);

            if (bikeFetchedById == null)
            {
                return null;
            }

            bikeFetchedById.brand = bike.brand;
            bikeFetchedById.model = bike.model;
            bikeFetchedById.price = bike.price;
            bikeFetchedById.availabilityInStore = bike.availabilityInStore;

            try
            {
                return await _bikeRepository.UpdateBikeById(id, bikeFetchedById);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot update bike by id {id}. {ex.Message}");
                throw;
            }
        }
    }
}
