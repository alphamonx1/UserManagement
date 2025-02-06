using CartService.Application.DTOs;
using CartService.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CartService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartRepository cartRepository) : ControllerBase
    {
        private readonly ICartRepository _cartRepository = cartRepository;

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            return Ok(cart);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequestDTO request)
        {
            await _cartRepository.AddToCartAsync(request.UserId, request.ProductId, request.Quantity);
            return Ok(new { message = "Product added to cart" });
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartRequestDTO request)
        {
            await _cartRepository.RemoveFromCartAsync(request.UserId, request.ProductId);
            return Ok(new { message = "Product removed from cart" });
        }

        [HttpDelete("clear/{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            await _cartRepository.ClearCartAsync(userId);
            return Ok(new { message = "Cart cleared" });
        }
    }
}
