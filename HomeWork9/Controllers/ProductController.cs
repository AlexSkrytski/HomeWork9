using HomeWork9.Models.DTOs;
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
    }
}
