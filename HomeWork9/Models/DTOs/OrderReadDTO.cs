namespace HomeWork9.Models.DTOs
{
    public record OrderReadDto
    {
       
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
