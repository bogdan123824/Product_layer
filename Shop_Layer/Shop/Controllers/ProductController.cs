using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.BusinessLayer.DTO;
using Shop.BusinessLayer.Interfaces;
using Shop.Models;

namespace Shop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProducts();
            var productResponses = _mapper.Map<List<ProductResponse>>(products);
            return Ok(productResponses);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetSingle([FromRoute] Guid id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            var productResponse = _mapper.Map<ProductResponse>(product);
            return Ok(productResponse);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
        {
            var productDto = _mapper.Map<ProductDTO>(request);
            try
            {
                var createdProduct = await _productService.CreateProduct(productDto);
                var productResponse = _mapper.Map<ProductResponse>(createdProduct);
                return CreatedAtAction(nameof(GetSingle), new { id = createdProduct.Id }, productResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductRequest request)
        {
            var productDto = _mapper.Map<ProductDTO>(request);
            productDto.Id = id;
            try
            {
                var updatedProduct = await _productService.UpdateProduct(productDto);
                var productResponse = _mapper.Map<ProductResponse>(updatedProduct);
                return Ok(productResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            try
            {
                await _productService.DeleteProduct(id);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
