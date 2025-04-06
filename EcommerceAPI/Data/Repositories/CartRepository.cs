using Microsoft.EntityFrameworkCore;
using EcommerceAPI.Models;

namespace EcommerceAPI.Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetUserCartAsync(int userId)
        {
            return await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<CartItem?> GetCartItemAsync(int cartId, int productId)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
        }

        public async Task<Cart> CreateCartAsync(int userId)
        {
            var cart = new Cart { UserId = userId };
            await _context.Carts.AddAsync(cart);
            return cart;
        }

        public async Task AddItemToCartAsync(Cart cart, CartItem item)
        {
            await _context.CartItems.AddAsync(item);
        }

        public Task UpdateCartItemAsync(CartItem item)
        {
            _context.CartItems.Update(item);
            return Task.CompletedTask;
        }

        public Task RemoveCartItemAsync(CartItem item)
        {
            _context.CartItems.Remove(item);
            return Task.CompletedTask;
        }

        public Task ClearCartAsync(Cart cart)
        {
            _context.CartItems.RemoveRange(cart.Items);
            return Task.CompletedTask;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
