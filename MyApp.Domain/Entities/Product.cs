namespace MyApp.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Unit { get; set; }
        public decimal Price { get; set; }
        public ICollection<OrderProducts> OrderProducts { get; } = [];
    }
}
