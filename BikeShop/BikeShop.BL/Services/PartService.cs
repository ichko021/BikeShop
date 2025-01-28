using BikeShop.BL.Interfaces;
using BikeShop.DL.Interfaces;
using BikeShop.DTO.DTO;

namespace BikeShop.BL.Services
{
    internal class PartService : IPartService
    {
        private readonly IPartRepository _partRepository;

        public PartService(IPartRepository partRepository) 
        {
            _partRepository = partRepository;
        }

        public void AddPart(Part part)
        {
            _partRepository.AddPart(part);
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

        public void UpdatePartById(string id, Part part)
        {
            _partRepository.UpdatePartById(id, part);
        }
    }
}
