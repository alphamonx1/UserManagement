namespace CartService.Application.DTOs
{
    public class CartResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<CartItemDTO> Items { get; set; }
    }
}
