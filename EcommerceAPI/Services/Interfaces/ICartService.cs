using EcommerceAPI.DTOs;
using EcommerceAPI.Helpers;

namespace EcommerceAPI.Services.Interfaces
{
    public interface ICartService
    {
        Task<EcommerceAPI.DTOs.ServiceResponse<CartDto>> GetUserCart(int userId);
        Task<EcommerceAPI.DTOs.ServiceResponse<CartDto>> AddToCart(int userId, AddToCartDto request);
        Task<EcommerceAPI.DTOs.ServiceResponse<CartDto>> UpdateQuantity(int userId, int productId, int quantity);
        Task<EcommerceAPI.DTOs.ServiceResponse<bool>> RemoveFromCart(int userId, int productId);
        Task<EcommerceAPI.DTOs.ServiceResponse<bool>> ClearCart(int userId);
    }
}
