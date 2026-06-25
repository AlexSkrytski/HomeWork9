namespace HomeWork9.Models.DTOs
{
    public record ProductCreateDto
    {
      
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
