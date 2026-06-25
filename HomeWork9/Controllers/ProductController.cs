using HomeWork9.Models.Entities;
using HomeWork9.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork9.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // URL: api/product
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        // Сервис автоматически внедряется через Dependency Injection (DI)
        public Dictionary<string, object> Dependencies => throw new NotImplementedException();

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        // 1. Получить все продукты (PING на GET)
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var products = _productService.HandleGetAllProductsPing();
                return Ok(products); // HTTP 200
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Внутренняя ошибка сервера: {ex.Message}");
            }
        }

        // 2. Добавить новый продукт (PING на POST)
        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            try
            {
                _productService.HandleAddProductPing(product);
                return CreatedAtAction(nameof(GetAll), new { id = product.Id }, product); // HTTP 201
            }
            catch (ArgumentNullException ex)
            {
                // Если передали null
                return BadRequest(new { error = ex.Message }); // HTTP 400
            }
            catch (InvalidOperationException ex)
            {
                // Если продукт с таким Id уже существует
                return Conflict(new { error = ex.Message }); // HTTP 409
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Непредвиденная ошибка: {ex.Message}" }); // HTTP 500
            }
        }

        // 3. Обновить продукт (PING на PUT)
        [HttpPut]
        public IActionResult Update([FromBody] Product product)
        {
            try
            {
                _productService.HandleUpdateProductPing(product);
                return Ok(product); // HTTP 200
            }
            catch (KeyNotFoundException ex)
            {
                // Если продукт для обновления не найден в списке
                return NotFound(new { error = ex.Message }); // HTTP 404
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = ex.Message }); // HTTP 400
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Непредвиденная ошибка: {ex.Message}" }); // HTTP 500
            }
        }
    }
}
