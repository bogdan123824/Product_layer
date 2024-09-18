using Shop.BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BusinessLayer.Interfaces
{
    public interface IManufacturerService
    {
        Task<IEnumerable<ManufacturerDTO>> GetAllManufacturers();
        Task<ManufacturerDTO> GetManufacturerById(Guid id);
        Task<ManufacturerDTO> CreateManufacturer(ManufacturerDTO newManufacturer);
        Task<ManufacturerDTO> UpdateManufacturer(ManufacturerDTO updatedManufacturer);
        Task DeleteManufacturer(Guid id);
        void Dispose();
    }
}
