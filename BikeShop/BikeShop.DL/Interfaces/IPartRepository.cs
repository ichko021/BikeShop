using BikeShop.DTO.POCO;

namespace BikeShop.DL.Interfaces
{
    public interface IPartRepository
    {
        Task<List<Part>> GetAllParts();
        Task<Part?> GetPartById(string id);
        Task DeletePartById(string id);
        Task<Part?> AddPart(Part part);
        Task<Part?> UpdatePartById(string id, Part part);
    }
}