using BikeShop.BL.Interfaces;
using BikeShop.DL.Interfaces;
using BikeShop.DTO.DTO;

namespace BikeShop.BL.Services
{
    public class PartService : IPartService
    {
        private readonly IPartRepository _partRepository;

        public PartService(IPartRepository partRepository) 
        {
            _partRepository = partRepository;
        }

        public Part? AddPart(Part part)
        {
            try
            {
                return _partRepository.AddPart(part);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeletePartById(string id)
        {
            _partRepository.DeletePartById(id);
        }

        public List<Part> GetAllParts()
        {
            return _partRepository.GetAllParts();
        }

        public Part? GetPartById(string id)
        {
            return _partRepository.GetPartById(id);
        }

        public Part? UpdatePartById(string id, Part part)
        {
            var partFetchedById = _partRepository.GetPartById(id);

            if (partFetchedById == null)
            {
                return null;
            }

            partFetchedById.partName = part.partName;
            partFetchedById.partSpec = part.partSpec;

            return _partRepository.UpdatePartById(id, partFetchedById);
        }
        
    }
}
