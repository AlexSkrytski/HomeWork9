using HomeWork9.Models.DTOs;
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

        public ProductReadDto HandleAddProductPing(ProductCreateDto dto)
        {
            try
            {
                if (dto == null) throw new ArgumentNullException(nameof(dto));

                // Маппинг: DTO -> Entity
                var productEntity = new Product
                {
                    Name = dto.Name,
                    Price = dto.Price
                };

                var savedProduct = _productRepository.Add(productEntity);

                // Маппинг: Entity -> DTO
                return new ProductReadDto
                {
                    Id = savedProduct.Id,
                    Name = savedProduct.Name,
                    Price = savedProduct.Price
                };
            }
            catch (Exception) { throw; }
        }

        public IEnumerable<ProductReadDto> HandleGetAllProductsPing()
        {
            return _productRepository.GetAll().Select(p => new ProductReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            });
        }

        public ProductReadDto HandleUpdateProductPing(Product product)
        {
            var updated = _productRepository.Update(product);
            return new ProductReadDto { Id = updated.Id, Name = updated.Name, Price = updated.Price };
        }

        public void HandleDeleteProductPing(int id)
        {
            _productRepository.Delete(id);
        }
    }
}
