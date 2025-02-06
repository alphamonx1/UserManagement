using CartService.Application.Repositories;
using CartService.Domain.Entities;
using StackExchange.Redis;
using System.Text.Json;

namespace CartService.Infrastructure.Repositories
{
    public class CartRepository(IDatabase redisDb) : ICartRepository
    {
        private readonly IDatabase _redisDb = redisDb;

        private static string GetCartKey(int userId)
        {
            return $"cart:{userId}";
        }

        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            var cartData = await _redisDb.StringGetAsync(GetCartKey(userId));
            return cartData.IsNullOrEmpty ? new Cart { UserId = userId } : JsonSerializer.Deserialize<Cart>(cartData);
        }

        public async Task AddToCartAsync(int userId, int productId, int quantity)
        {
            var cart = await GetCartByUserIdAsync(userId);
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (existingItem != null)
                existingItem.Quantity += quantity;
            else
                cart.Items.Add(new CartItem { ProductId = productId, Quantity = quantity });

            await _redisDb.StringSetAsync(GetCartKey(userId), JsonSerializer.Serialize(cart));
        }

        public async Task RemoveFromCartAsync(int userId, int productId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            cart.Items.RemoveAll(i => i.ProductId == productId);
            await _redisDb.StringSetAsync(GetCartKey(userId), JsonSerializer.Serialize(cart));
        }

        public async Task ClearCartAsync(int userId)
        {
            await _redisDb.KeyDeleteAsync(GetCartKey(userId));
        }
    }
}
