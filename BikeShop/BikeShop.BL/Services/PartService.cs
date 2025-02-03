using BikeShop.BL.Interfaces;
using BikeShop.DL.Interfaces;
using BikeShop.DTO.DTO;
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

        public Part? AddPart(Part part)
        {
            try
            {
                return _partRepository.AddPart(part);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot add bike. {ex.Message} | {ex.StackTrace}");
                throw;
            }
        }

        public void DeletePartById(string id)
        {
            try
            {
                _partRepository.DeletePartById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot delete part. {ex.Message} | {ex.StackTrace}");
                throw;
            }

        }

        public List<Part> GetAllParts()
        {
            try
            {
                return _partRepository.GetAllParts();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot fetch parts. {ex.Message} | {ex.StackTrace}");
                throw;
            }
        }

        public Part? GetPartById(string id)
        {
            try
            {
                return _partRepository.GetPartById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot fetch part by {id}. {ex.Message} | {ex.StackTrace}");
                throw;
            }
            
        }

        public Part? UpdatePartById(string id, Part part)
        {
            var partFetchedById = GetPartById(id);

            if (partFetchedById == null)
            {
                return null;
            }

            partFetchedById.partName = part.partName;
            partFetchedById.partSpec = part.partSpec;

            try
            {
                return _partRepository.UpdatePartById(id, partFetchedById);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot update part by {id}. {ex.Message} | {ex.StackTrace}");
                throw;
            }
        }
        
    }
}
