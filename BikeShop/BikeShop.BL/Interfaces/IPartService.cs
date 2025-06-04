using BikeShop.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeShop.BL.Interfaces
{
    public interface IPartService
    {
        Task<List<Part>> GetAllParts();
        Task<Part?> GetPartById(string id);
        Task DeletePartById(string id);
        Task<Part?> AddPart(Part part);
        Task<Part?> UpdatePartById(string id, Part part);
    }
}
