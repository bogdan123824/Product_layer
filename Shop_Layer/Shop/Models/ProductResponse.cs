using Shop.DataAccessLayer.Entities;

namespace Shop.Models
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int Price { get; set; } = int.MaxValue;
        public string? Category { get; set; }
        public string? Manufacturer { get; set; }
    }
}
