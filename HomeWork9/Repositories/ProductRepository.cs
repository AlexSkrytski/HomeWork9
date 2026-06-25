using HomeWork9.Models.Entities;
using HomeWork9.Repositories.Interfaces;

namespace HomeWork9.Repositories
{
    public class ProductRepository : InMemoryRepository<Product>, IProductOperations
    {
        public bool CheckStockAvailability(int productId, int requiredQuantity)
        {
            var product = GetById(productId);
            // Пример логики
            return product != null && requiredQuantity > 0;
        }
    }
}
