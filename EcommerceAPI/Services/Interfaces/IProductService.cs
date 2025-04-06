using EcommerceAPI.DTOs;
using EcommerceAPI.Helpers;

namespace EcommerceAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task<EcommerceAPI.DTOs.ServiceResponse<List<ProductDto>>> GetAllProducts();
        Task<EcommerceAPI.DTOs.ServiceResponse<ProductDto>> GetProductById(int id);
        Task<EcommerceAPI.DTOs.ServiceResponse<ProductDto>> CreateProduct(CreateProductDto product);
        Task<EcommerceAPI.DTOs.ServiceResponse<ProductDto>> UpdateProduct(int id, UpdateProductDto product);
        Task<EcommerceAPI.DTOs.ServiceResponse<bool>> DeleteProduct(int id);
    Task UpdateQuantity(int productId, int v);
    Task UpdateProduct(int id, CreateProductDto productDto);
  }
}
