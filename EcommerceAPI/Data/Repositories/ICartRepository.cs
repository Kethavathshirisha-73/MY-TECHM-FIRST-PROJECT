using EcommerceAPI.Models;

namespace EcommerceAPI.Data.Repositories
{
    public interface ICartRepository
    {
        Task<Cart?> GetUserCartAsync(int userId);
        Task<CartItem?> GetCartItemAsync(int cartId, int productId);
        Task<Cart> CreateCartAsync(int userId);
        Task AddItemToCartAsync(Cart cart, CartItem item);
        Task UpdateCartItemAsync(CartItem item);
        Task RemoveCartItemAsync(CartItem item);
        Task ClearCartAsync(Cart cart);
        Task<bool> SaveChangesAsync();
    }
}
