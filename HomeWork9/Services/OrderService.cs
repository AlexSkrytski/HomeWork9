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

        public void HandleAddOrderPing(Order order)
        {
            try
            {
                _orderRepository.Add(order);
                Console.WriteLine("Заказ успешно создан.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании заказа: {ex.Message}");
                throw;
            }
        }

        // Обработка пинга архивации
        public void HandleArchiveOrderPing(int orderId)
        {
            try
            {
                _orderRepository.ArchiveOrder(orderId);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Ошибка архивации: {ex.Message}");
                throw;
            }
        }
    }
}
