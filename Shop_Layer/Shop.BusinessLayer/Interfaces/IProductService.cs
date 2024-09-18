using Shop.BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BusinessLayer.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProducts();
        Task<ProductDTO> GetProductById(Guid id);
        Task<ProductDTO> CreateProduct(ProductDTO newProduct);
        Task<ProductDTO> UpdateProduct(ProductDTO updatedProduct);
        Task DeleteProduct(Guid id);
        void Dispose();
    }
}
