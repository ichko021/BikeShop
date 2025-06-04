using BikeShop.BL.Interfaces;
using BikeShop.DL.Interfaces;
using BikeShop.DTO.POCO;
using Microsoft.Extensions.Logging;

namespace BikeShop.BL.Services
{
    public class PartService : IPartService
    {
        private readonly IPartRepository _partRepository;
        private readonly ILogger<PartService> _logger;

        public PartService(IPartRepository partRepository, ILogger<PartService> logger)
        {
            _partRepository = partRepository;
            _logger = logger;
        }

        public async Task<Part?> AddPart(Part part)
        {
            try
            {
                return await _partRepository.AddPart(part);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot add part. {ex.Message}");
                throw;
            }
        }

        public async Task DeletePartById(string id)
        {
            try
            {
                await _partRepository.DeletePartById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot delete part by id {id}. {ex.Message}");
                throw;
            }
        }

        public async Task<List<Part>> GetAllParts()
        {
            try
            {
                return await _partRepository.GetAllParts();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot fetch parts. {ex.Message}");
                throw;
            }
        }

        public async Task<Part?> GetPartById(string id)
        {
            try
            {
                return await _partRepository.GetPartById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot fetch part by id {id}. {ex.Message}");
                throw;
            }
        }

        public async Task<Part?> UpdatePartById(string id, Part part)
        {
            var partFetchedById = await GetPartById(id);

            if (partFetchedById == null)
            {
                return null;
            }

            partFetchedById.partName = part.partName;
            partFetchedById.partSpec = part.partSpec;

            try
            {
                return await _partRepository.UpdatePartById(id, partFetchedById);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot update part by id {id}. {ex.Message}");
                throw;
            }
        }
    }
}
