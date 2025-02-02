namespace ProductService.Application.DTOs
{
    public class ProductResponseDTO
    {
        /// <summary>
        /// Id of the product
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Name of the product
        /// </summary>
        public string? ProductName { get; set; }
        /// <summary>
        /// Description of the product
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Image of the product
        /// </summary>
        public string? ImageUrl { get; set; }
        /// <summary>
        /// Price of the product
        /// </summary>
        public float Price { get; set; }
        /// <summary>
        /// Quantity of the product
        /// </summary>
        public int Quantity { get; set; }
    }
}
