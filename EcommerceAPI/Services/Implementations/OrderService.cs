using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EcommerceAPI.Data;
using EcommerceAPI.DTOs;
using EcommerceAPI.Models;
using EcommerceAPI.Helpers;
using EcommerceAPI.Services.Interfaces;

namespace EcommerceAPI.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public OrderService(
            ApplicationDbContext context,
            IMapper mapper,
            IProductService productService)
        {
            _context = context;
            _mapper = mapper;
            _productService = productService;
        }

        public async Task<EcommerceAPI.DTOs.ServiceResponse<OrderDto>> CreateOrder(int userId, CreateOrderDto request)
        {
            var response = new EcommerceAPI.DTOs.ServiceResponse<OrderDto>();
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Get user's cart
                var cart = await _context.Carts
                    .Include(c => c.Items)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart == null || !cart.Items.Any())
                {
                    response.Success = false;
                    response.Message = "Cart is empty";
                    return response;
                }

                // Create order
                var order = new Order
                {
                    UserId = userId,
                    OrderNumber = GenerateOrderNumber(),
                    ShippingAddress = request.ShippingAddress,
                    Notes = request.Notes,
                    Status = "Pending",
                    OrderDate = DateTime.UtcNow
                };

                // Add order items and calculate total
                foreach (var cartItem in cart.Items)
                {
                    // Check stock
                    if (cartItem.Product.StockQuantity < cartItem.Quantity)
                    {
                        response.Success = false;
                        response.Message = $"Insufficient stock for product: {cartItem.Product.Name}";
                        return response;
                    }

                    var orderItem = new OrderItem
                    {
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        Price = cartItem.Product.Price
                    };

                    order.Items.Add(orderItem);
                    order.TotalAmount += orderItem.Price * orderItem.Quantity;

                    // Update product stock
                    //await _productService.UpdateQuantity(cartItem.ProductId, -cartItem.Quantity);
                }

                _context.Orders.Add(order);

                // Clear the cart
                _context.CartItems.RemoveRange(cart.Items);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Map to DTO
                var orderDto = _mapper.Map<OrderDto>(order);
                response.Data = orderDto;
                response.Message = "Order created successfully";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<EcommerceAPI.DTOs.ServiceResponse<List<OrderDto>>> GetUserOrders(int userId)
        {
            var response = new EcommerceAPI.DTOs.ServiceResponse<List<OrderDto>>();
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .Where(o => o.UserId == userId)
                    .OrderByDescending(o => o.OrderDate)
                    .ToListAsync();

                response.Data = _mapper.Map<List<OrderDto>>(orders);
                response.Message = "Orders retrieved successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<EcommerceAPI.DTOs.ServiceResponse<OrderDto>> GetOrderById(int userId, int orderId)
        {
            var response = new EcommerceAPI.DTOs.ServiceResponse<OrderDto>();
            try
            {
                var order = await _context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

                if (order == null)
                {
                    response.Success = false;
                    response.Message = "Order not found";
                    return response;
                }

                response.Data = _mapper.Map<OrderDto>(order);
                response.Message = "Order retrieved successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<EcommerceAPI.DTOs.ServiceResponse<OrderDto>> UpdateOrderStatus(int orderId, string status)
        {
            var response = new EcommerceAPI.DTOs.ServiceResponse<OrderDto>();
            try
            {
                var order = await _context.Orders.FindAsync(orderId);
                if (order == null)
                {
                    response.Success = false;
                    response.Message = "Order not found";
                    return response;
                }

                order.Status = status;
                order.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<OrderDto>(order);
                response.Message = "Order status updated successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        private string GenerateOrderNumber()
        {
            return $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8)}";
        }

    public Task CreateOrder(int userId)
    {
      throw new NotImplementedException();
    }
  }
}
