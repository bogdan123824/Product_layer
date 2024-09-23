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
        /// <summary>
        /// Запрос на список всех продуктов.
        /// </summary>
        /// <returns>
        /// Если нет продуктов то  возвращаеться пустой список
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProducts();
            var productResponses = _mapper.Map<List<ProductResponse>>(products);
            return Ok(productResponses);
        }

        /// <summary>
        /// Запрос на продукт по его Айди.
        /// </summary>
        /// <param name="id">Айди продукта</param>
        /// <returns>
        /// Возвращаеться код 200 если продукт с таким айди есть в списке. 
        /// Возвращаеться код 404 если продукт не найден.
        /// </returns>
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
        /// <summary>
        /// Запрос на добавление продукта.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Product
        ///     {
        ///        "name" : "A4Tech Bloody B188",
        ///        "text" : "blalsdlasd"
        ///        "price" : 111,
        ///        "category": "PeripheryAndAccessories",
        ///        "manufacturer" : "Pepsi"
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Модель для добавления продукта <see cref="AddProductRequest"/></param>
        /// <returns>Возвращаеться код 201 при успешном добавлении продукта.
        /// Возвращаеться код 400 при неправильном добавлении продукта.
        /// </returns>
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
        /// <summary>
        /// Запрос на редактирование существующего продукта в списке.
        /// </summary>
        /// <param name="id">Айди каждого существующего продукта</param>
        /// <param name="request">Редактирование продукта по модели <see cref="UpdateProductRequest"/></param>
        /// <returns>Возвращаеться код 200 при успешном редактировании продукта.
        /// Возвращаеться код 400 при неправильном редактировании продукта.
        /// </returns>
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
        /// <summary>
        /// Запрос на удаление продукта по Айди.
        /// </summary>
        /// <param name="id">Айди каждого существующего продукта</param>
        /// <returns>Возвращаеться код 204 при успешном удалении продукта.
        /// Возвращаеться код 400 при неправильном удалении продукта.
        /// </returns>
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
