namespace CartService.Application.DTOs
{
    public class RemoveFromCartRequestDTO
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
