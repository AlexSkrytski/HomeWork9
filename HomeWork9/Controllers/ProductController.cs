using HomeWork9.Models.DTOs;
using HomeWork9.Models.Entities;
using HomeWork9.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork9.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var products = _productService.HandleGetAllProductsPing();
                return Ok(products); // Возвращает IEnumerable<ProductReadDto>
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductCreateDto dto) // Принимает ProductCreateDto
        {
            try
            {
                var result = _productService.HandleAddProductPing(dto);
                return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result); // Возвращает ProductReadDto
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("{id}")] // URL: api/product/5
        public IActionResult Update(int id, [FromBody] ProductCreateDto dto)
        {
            try
            {
                // Для обновления нам нужно передать Id сущности в репозиторий.
                // Маппим DTO обратно в Entity, подставляя ID из URL.
                var productToUpdate = new Product
                {
                    Id = id,
                    Name = dto.Name,
                    Price = dto.Price
                };

                var updatedProduct = _productService.HandleUpdateProductPing(productToUpdate);
                return Ok(updatedProduct); // HTTP 200
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message }); // HTTP 404, если Id не найден
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = ex.Message }); // HTTP 400
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Непредвиденная ошибка: {ex.Message}" });
            }
        }

        // 4. Удалить продукт (PING на DELETE)
        [HttpDelete("{id}")] // URL: api/product/5
        public IActionResult Delete(int id)
        {
            try
            {
                _productService.HandleDeleteProductPing(id);
                return NoContent(); // HTTP 204 No Content (стандарт для успешного удаления без возврата тела)
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message }); // HTTP 404
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Ошибка при удалении: {ex.Message}" });
            }
        }
    }
}
