using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using EcommerceAPI.Data;
using EcommerceAPI.DTOs;
using EcommerceAPI.Services;
using EcommerceAPI.Services.Interfaces;

namespace EcommerceAPI.Services.Implementations
{
    public class CartService: ICartService
    {
        private readonly EcommerceAPI.Data.ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CartService(EcommerceAPI.Data.ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

    public Task<DTOs.ServiceResponse<CartDto>> AddToCart(int userId, AddToCartDto request)
    {
      throw new NotImplementedException();
    }

    public Task<DTOs.ServiceResponse<bool>> ClearCart(int userId)
    {
      throw new NotImplementedException();
    }

    public async Task<EcommerceAPI.DTOs.ServiceResponse<CartDto>> GetUserCart(int userId)
        {
            var response = new EcommerceAPI.DTOs.ServiceResponse<CartDto>();
            try
            {
                var cart = await _context.Carts
                    .Include(c => c.Items)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart == null)
                {
                    cart = new Cart { UserId = userId };
                    _context.Carts.Add(cart);
                    await _context.SaveChangesAsync();
                }

                response.Data = _mapper.Map<CartDto>(cart); // Using AutoMapper
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

    public Task<DTOs.ServiceResponse<bool>> RemoveFromCart(int userId, int productId)
    {
      throw new NotImplementedException();
    }

    public Task<DTOs.ServiceResponse<CartDto>> UpdateQuantity(int userId, int productId, int quantity)
    {
      throw new NotImplementedException();
    }
  }
}
