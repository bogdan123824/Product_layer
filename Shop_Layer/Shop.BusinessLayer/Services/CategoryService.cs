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
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            var categories = await _unitOfWork.Categories.GetAll();
            var categoriesDto = _mapper.Map<List<CategoryDTO>>(categories);
            return categoriesDto;
        }

        public async Task<CategoryDTO> GetCategoryById(Guid id)
        {
            var category = await _unitOfWork.Categories.Get(id);

            if (category == null)
            {
                throw new Exception($"Category ID {id} not found");
            }

            var categoryDto = _mapper.Map<CategoryDTO>(category);
            return categoryDto;
        }

        public async Task<CategoryDTO> CreateCategory(CategoryDTO newCategory)
        {
            var category = _mapper.Map<Category>(newCategory);

            await _unitOfWork.Categories.Create(category);
            await _unitOfWork.SaveChanges();

            return newCategory;
        }

        public async Task<CategoryDTO> UpdateCategory(CategoryDTO updatedCategory)
        {
            var categoryExists = await _unitOfWork.Categories.Get(updatedCategory.Id) != null;

            if (!categoryExists)
            {
                throw new Exception($"Category ID {updatedCategory.Id} not found");
            }

            var category = _mapper.Map<Category>(updatedCategory);

            await _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveChanges();

            return updatedCategory;
        }

        public async Task DeleteCategory(Guid id)
        {
            var categoryExists = await _unitOfWork.Categories.Get(id) != null;

            if (!categoryExists)
            {
                throw new Exception($"Category ID {id} not found");
            }

            await _unitOfWork.Categories.Delete(id);
            await _unitOfWork.SaveChanges();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }

}
