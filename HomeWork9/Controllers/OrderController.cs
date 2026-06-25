using HomeWork9.Models.DTOs;
using HomeWork9.Models.Entities;
using HomeWork9.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork9.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // URL: api/order
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // 1. Получить все заказы (PING на GET)
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var orders = _orderService.HandleGetAllOrdersPing();
                return Ok(orders); // Возвращает IEnumerable<OrderReadDto> с HTTP 200
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Внутренняя ошибка сервера: {ex.Message}" });
            }
        }

        // 2. Создать новый заказ (PING на POST)
        [HttpPost]
        public IActionResult Create([FromBody] OrderCreateDto dto) // Теперь принимает OrderCreateDto
        {
            try
            {
                var result = _orderService.HandleAddOrderPing(dto);
                return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result); // Возвращает OrderReadDto с HTTP 201
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = ex.Message }); // HTTP 400
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message }); // HTTP 400 для невалидной суммы
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Ошибка при создании заказа: {ex.Message}" }); // HTTP 500
            }
        }

        // 3. Отправить заказ в архив (PING на POST с ID в URL)
        [HttpPost("{id}/archive")] // URL: api/order/5/archive
        public IActionResult Archive(int id)
        {
            try
            {
                _orderService.HandleArchiveOrderPing(id);
                return Ok(new { message = $"Заказ {id} успешно архивирован." }); // HTTP 200
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message }); // HTTP 400 при некорректном Id (<= 0)
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message }); // HTTP 404 если заказ не найден
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Ошибка при архивации: {ex.Message}" }); // HTTP 500
            }
        }
    }
}
