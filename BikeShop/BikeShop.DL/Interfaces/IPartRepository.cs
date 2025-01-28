using BikeShop.DTO.DTO;

namespace BikeShop.DL.Interfaces
{
    public interface IPartRepository
    {
        List<Part> GetAllParts();
        Part? GetPartById(string id);
        void DeletePartById(string id);
        void AddPart(Part part);
        void UpdatePartById(string id, Part part);
    }
}