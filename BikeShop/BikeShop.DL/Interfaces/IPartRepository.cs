using BikeShop.DTO.DTO;

namespace BikeShop.DL.Interfaces
{
    public interface IPartRepository
    {
        List<Part> GetAllParts();
        Part? GetPartById(string id);
        void DeletePartById(string id);
        Part? AddPart(Part part);
        Part? UpdatePartById(string id, Part part);
    }
}