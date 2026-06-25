namespace HomeWork9.Models.DTOs
{
    public record ProductReadDto
    {
        
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
