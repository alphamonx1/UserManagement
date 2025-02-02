using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Domain.Entities
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
