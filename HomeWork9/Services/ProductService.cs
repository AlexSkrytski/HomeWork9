using HomeWork9.Models.Entities;
using HomeWork9.Repositories;

namespace HomeWork9.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Симуляция пинга для добавления
        public void HandleAddProductPing(Product product)
        {
            try
            {
                _productRepository.Add(product);
                Console.WriteLine("Продукт успешно добавлен.");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Ошибка валидации: {ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Конфликт данных: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
                throw;
            }
        }

        // Симуляция пинга для обновления
        public void HandleUpdateProductPing(Product product)
        {
            try
            {
                _productRepository.Update(product);
                Console.WriteLine("Продукт успешно обновлен.");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Ошибка обновления: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<Product> HandleGetAllProductsPing()
        {
            return _productRepository.GetAll();
        }
    }
}
