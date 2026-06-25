using HomeWork9.Models.DTOs;
using HomeWork9.Models.Entities;
using HomeWork9.Repositories;

namespace HomeWork9.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // 1. Создание заказа (Принимает Create DTO -> Возвращает Read DTO)
        public OrderReadDto HandleAddOrderPing(OrderCreateDto dto)
        {
            try
            {
                if (dto == null)
                    throw new ArgumentNullException(nameof(dto), "Данные заказа не могут быть пустыми.");

                if (dto.TotalAmount <= 0)
                    throw new ArgumentException("Сумма заказа должна быть больше нуля.");

                // Маппинг: DTO -> Entity (Id сгенерирует репозиторий, дату ставим текущую)
                var orderEntity = new Order
                {
                    OrderDate = DateTime.UtcNow,
                    TotalAmount = dto.TotalAmount
                };

                var savedOrder = _orderRepository.Add(orderEntity);

                // Маппинг: Entity -> DTO
                return new OrderReadDto
                {
                    Id = savedOrder.Id,
                    OrderDate = savedOrder.OrderDate,
                    TotalAmount = savedOrder.TotalAmount
                };
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка валидации бизнес-логики: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредвиденная ошибка при создании заказа: {ex.Message}");
                throw;
            }
        }

        // 2. Получение всех заказов (Конвертация всего списка в Read DTO)
        public IEnumerable<OrderReadDto> HandleGetAllOrdersPing()
        {
            try
            {
                return _orderRepository.GetAll().Select(order => new OrderReadDto
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount
                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении списка заказов: {ex.Message}");
                throw;
            }
        }

        // 3. Архивация заказа (Специфичный метод обработки информации)
        public void HandleArchiveOrderPing(int orderId)
        {
            try
            {
                if (orderId <= 0)
                    throw new ArgumentException("Некорректный ID заказа.", nameof(orderId));

                _orderRepository.ArchiveOrder(orderId);
                Console.WriteLine($"Заказ {orderId} успешно обработан сервисом архивации.");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Ошибка архивации в сервисе: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Критическая ошибка архивации: {ex.Message}");
                throw;
            }
        }
    }
}
