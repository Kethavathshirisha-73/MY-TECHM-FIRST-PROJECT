// Controllers/CartController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EcommerceAPI.DTOs;
using EcommerceAPI.Services;
using EcommerceAPI.Helpers;

namespace EcommerceAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly EcommerceAPI.Services.Interfaces.ICartService _cartService;

        public CartController(EcommerceAPI.Services.Interfaces.ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<ActionResult<EcommerceAPI.DTOs.ServiceResponse<CartDto>>> GetUserCart()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var response = await _cartService.GetUserCart(userId);
            return Ok(response);
        }

        [HttpPost("add")]
        public async Task<ActionResult<EcommerceAPI.DTOs.ServiceResponse<CartDto>>> AddToCart(AddToCartDto request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var response = await _cartService.AddToCart(userId, request);
            return Ok(response);
        }

        [HttpPut("update-quantity")]
        public async Task<ActionResult<EcommerceAPI.DTOs.ServiceResponse<CartDto>>> UpdateQuantity(int productId, int quantity)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var response = await _cartService.UpdateQuantity(userId, productId, quantity);
            return Ok(response);
        }

        [HttpDelete("remove/{productId}")]
        public async Task<ActionResult<EcommerceAPI.DTOs.ServiceResponse<bool>>> RemoveFromCart(int productId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var response = await _cartService.RemoveFromCart(userId, productId);
            return Ok(response);
        }

        [HttpDelete("clear")]
        public async Task<ActionResult<EcommerceAPI.DTOs.ServiceResponse<bool>>> ClearCart()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var response = await _cartService.ClearCart(userId);
            return Ok(response);
        }
    }
}