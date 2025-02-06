namespace CartService.Application.DTOs
{
    public class AddToCartRequestDTO
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
