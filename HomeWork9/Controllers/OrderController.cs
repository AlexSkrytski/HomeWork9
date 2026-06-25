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

        // Внедрение зависимости через конструктор
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // 1. Создать новый заказ (PING на POST)
        [HttpPost]
        public IActionResult Create([FromBody] Order order)
        {
            try
            {
                _orderService.HandleAddOrderPing(order);
                return StatusCode(201, order); // HTTP 201 Created
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = ex.Message }); // HTTP 400
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message }); // HTTP 409
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Ошибка при создании заказа: {ex.Message}" });
            }
        }

        // 2. Отправить заказ в архив (PING на POST с ID в URL)
        // Использует специфичный метод из интерфейса IOrderOperations через сервис
        [HttpPost("{id}/archive")] // URL: api/order/5/archive
        public IActionResult Archive(int id)
        {
            try
            {
                _orderService.HandleArchiveOrderPing(id);
                return Ok(new { message = $"Заказ {id} успешно архивирован." }); // HTTP 200
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message }); // HTTP 404
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Ошибка при архивации: {ex.Message}" });
            }
        }
    }
}
