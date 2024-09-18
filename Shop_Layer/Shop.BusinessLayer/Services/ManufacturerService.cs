using AutoMapper;
using Shop.BusinessLayer.DTO;
using Shop.BusinessLayer.Interfaces;
using Shop.DataAccessLayer.Entities;
using Shop.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BusinessLayer.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ManufacturerService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ManufacturerDTO>> GetAllManufacturers()
        {
            var manufacturers = await _unitOfWork.Manufacturers.GetAll();
            var manufacturersDto = _mapper.Map<List<ManufacturerDTO>>(manufacturers);
            return manufacturersDto;
        }

        public async Task<ManufacturerDTO> GetManufacturerById(Guid id)
        {
            var manufacturer = await _unitOfWork.Manufacturers.Get(id);

            if (manufacturer == null)
            {
                throw new Exception($"Manufacturer ID  {id}  not found");
            }

            var manufacturerDto = _mapper.Map<ManufacturerDTO>(manufacturer);
            return manufacturerDto;
        }

        public async Task<ManufacturerDTO> CreateManufacturer(ManufacturerDTO newManufacturer)
        {
            var manufacturer = _mapper.Map<Manufacturer>(newManufacturer);

            await _unitOfWork.Manufacturers.Create(manufacturer);
            await _unitOfWork.SaveChanges();

            return newManufacturer;
        }

        public async Task<ManufacturerDTO> UpdateManufacturer(ManufacturerDTO updatedManufacturer)
        {
            var manufacturerExists = await _unitOfWork.Manufacturers.Get(updatedManufacturer.Id) != null;

            if (!manufacturerExists)
            {
                throw new Exception($"Manufacturer ID {updatedManufacturer.Id} not found");
            }

            var manufacturer = _mapper.Map<Manufacturer>(updatedManufacturer);

            await _unitOfWork.Manufacturers.Update(manufacturer);
            await _unitOfWork.SaveChanges();

            return updatedManufacturer;
        }

        public async Task DeleteManufacturer(Guid id)
        {
            var manufacturerExists = await _unitOfWork.Manufacturers.Get(id) != null;

            if (!manufacturerExists)
            {
                throw new Exception($"Manufacturer ID {id} not found");
            }

            await _unitOfWork.Manufacturers.Delete(id);
            await _unitOfWork.SaveChanges();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }

}
