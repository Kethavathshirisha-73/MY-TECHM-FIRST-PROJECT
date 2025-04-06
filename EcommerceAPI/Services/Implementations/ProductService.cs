using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EcommerceAPI.Data;
using EcommerceAPI.DTOs;
using EcommerceAPI.Models;
using EcommerceAPI.Helpers;
using EcommerceAPI.Services.Interfaces;

namespace EcommerceAPI.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            
            _mapper = mapper;
        }

        public async Task<EcommerceAPI.DTOs.ServiceResponse<List<ProductDto>>> GetAllProducts()
        {
            var response = new EcommerceAPI.DTOs.ServiceResponse<List<ProductDto>>();
            try
            {
                var products = await _context.Products
                    .Where(p => p.IsAvailable)
                    .OrderByDescending(p => p.CreatedAt)
                    .ToListAsync();

                response.Data = _mapper.Map<List<ProductDto>>(products);
                response.Message = "Products retrieved successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<EcommerceAPI.DTOs.ServiceResponse<ProductDto>> GetProductById(int id)
        {
            var response = new EcommerceAPI.DTOs.ServiceResponse<ProductDto>();
            try
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == id && p.IsAvailable);

                if (product == null)
                {
                    response.Success = false;
                    response.Message = "Product not found";
                    return response;
                }

                response.Data = _mapper.Map<ProductDto>(product);
                response.Message = "Product retrieved successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<EcommerceAPI.DTOs.ServiceResponse<ProductDto>> CreateProduct(CreateProductDto productDto)
        {
            var response = new EcommerceAPI.DTOs.ServiceResponse<ProductDto>();
            try
            {
                var product = _mapper.Map<Product>(productDto);
                product.CreatedAt = DateTime.UtcNow;
                product.UpdatedAt = DateTime.UtcNow;
                product.IsAvailable = true;

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<ProductDto>(product);
                response.Message = "Product created successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<EcommerceAPI.DTOs.ServiceResponse<ProductDto>> UpdateProduct(int id, UpdateProductDto productDto)
        {
            var response = new EcommerceAPI.DTOs.ServiceResponse<ProductDto>();
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    response.Success = false;
                    response.Message = "Product not found";
                    return response;
                }

                _mapper.Map(productDto, product);
                product.UpdatedAt = DateTime.UtcNow;

                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<ProductDto>(product);
                response.Message = "Product updated successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<EcommerceAPI.DTOs.ServiceResponse<bool>> DeleteProduct(int id)
        {
            var response = new EcommerceAPI.DTOs.ServiceResponse<bool>();
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    response.Success = false;
                    response.Message = "Product not found";
                    return response;
                }

                // Soft delete - just mark as unavailable
                product.IsAvailable = false;
                product.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                response.Data = true;
                response.Message = "Product deleted successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<EcommerceAPI.DTOs.ServiceResponse<bool>> UpdateStock(int id, int quantity)
        {
            var response = new EcommerceAPI.DTOs.ServiceResponse<bool>();
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    response.Success = false;
                    response.Message = "Product not found";
                    return response;
                }

                product.StockQuantity += quantity;
                product.UpdatedAt = DateTime.UtcNow;

                if (product.StockQuantity < 0)
                {
                    response.Success = false;
                    response.Message = "Insufficient stock";
                    return response;
                }

                await _context.SaveChangesAsync();

                response.Data = true;
                response.Message = "Stock updated successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

    public Task UpdateQuantity(int productId, int v)
    {
      throw new NotImplementedException();
    }

    public Task UpdateProduct(int id, CreateProductDto productDto)
    {
      throw new NotImplementedException();
    }
  }
}
