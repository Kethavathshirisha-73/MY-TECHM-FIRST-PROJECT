using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EcommerceAPI.DTOs;
using EcommerceAPI.Services.Interfaces;
using EcommerceAPI.Helpers;

namespace EcommerceAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        /*public async Task<ActionResult<EcommerceAPI.DTOs.ServiceResponse<int>>> CreateOrder()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
          
            var response = await _orderService.CreateOrder(userId);
            return Ok(response);
        }*/
        

        [HttpGet]
        public async Task<ActionResult<EcommerceAPI.DTOs.ServiceResponse<List<OrderDto>>>> GetUserOrders()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var response = await _orderService.GetUserOrders(userId);
            return Ok(response);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<EcommerceAPI.DTOs.ServiceResponse<OrderDto>>> GetOrderById(int orderId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var response = await _orderService.GetOrderById(userId, orderId);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
