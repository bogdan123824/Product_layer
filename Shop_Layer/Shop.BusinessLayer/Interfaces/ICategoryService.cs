using Shop.BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BusinessLayer.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategories();
        Task<CategoryDTO> GetCategoryById(Guid id);
        Task<CategoryDTO> CreateCategory(CategoryDTO newCategory);
        Task<CategoryDTO> UpdateCategory(CategoryDTO updatedCategory);
        Task DeleteCategory(Guid id);
        void Dispose();
    }
}
