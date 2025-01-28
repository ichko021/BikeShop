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
        List<Part> GetAllParts();
        Part? GetPartById(string id);
        void DeletePartById(string id);
        void AddPart(Part part);
        void UpdatePartById(string id, Part part);
    }
}
