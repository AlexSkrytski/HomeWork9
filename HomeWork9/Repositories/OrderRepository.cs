using HomeWork9.Models.Entities;
using HomeWork9.Repositories.Interfaces;

namespace HomeWork9.Repositories
{
    public class OrderRepository : InMemoryRepository<Order>, IOrderOperations
    {
        public void ArchiveOrder(int orderId)
        {
            var order = GetById(orderId);
            // Логика перевода заказа в архив
            Console.WriteLine($"Заказ {orderId} отправлен в архив.");
        }
    }
