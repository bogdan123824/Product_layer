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
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            var products = await _unitOfWork.Products.GetAll();
            var productsDto = _mapper.Map<List<ProductDTO>>(products);
            return productsDto;
        }

        public async Task<ProductDTO> GetProductById(Guid id)
        {
            var product = await _unitOfWork.Products.Get(id);

            if (product == null)
            {
                throw new Exception($"Product ID  {id}  not found");
            }

            var productDto = _mapper.Map<ProductDTO>(product);
            return productDto;
        }

        public async Task<ProductDTO> CreateProduct(ProductDTO newProduct)
        {
            var product = _mapper.Map<Product>(newProduct);

            await _unitOfWork.Products.Create(product);
            await _unitOfWork.SaveChanges();

            return newProduct;
        }

        public async Task<ProductDTO> UpdateProduct(ProductDTO updatedProduct)
        {
            var productExists = await _unitOfWork.Products.Get(updatedProduct.Id) != null;

            if (!productExists)
            {
                throw new Exception($"Product ID {updatedProduct.Id} not found");
            }

            var product = _mapper.Map<Product>(updatedProduct);

            await _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveChanges();

            return updatedProduct;
        }

        public async Task DeleteProduct(Guid id)
        {
            var productExists = await _unitOfWork.Products.Get(id) != null;

            if (!productExists)
            {
                throw new Exception($"Product ID {id} not found");
            }

            await _unitOfWork.Products.Delete(id);
            await _unitOfWork.SaveChanges();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }

}
