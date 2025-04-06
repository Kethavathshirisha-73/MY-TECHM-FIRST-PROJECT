using EcommerceAPI.DTOs;
using EcommerceAPI.Helpers;

namespace EcommerceAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<EcommerceAPI.DTOs.ServiceResponse<OrderDto>> CreateOrder(int userId , CreateOrderDto request);
        Task<EcommerceAPI.DTOs.ServiceResponse<List<OrderDto>>> GetUserOrders(int userId);
        Task<EcommerceAPI.DTOs.ServiceResponse<OrderDto>> GetOrderById(int userId, int orderId);
    Task CreateOrder(int userId);
  }
}
