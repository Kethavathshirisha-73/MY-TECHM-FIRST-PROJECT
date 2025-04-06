using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EcommerceAPI.DTOs;
using EcommerceAPI.Services;
using EcommerceAPI.Helpers;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly EcommerceAPI.Services.Interfaces.IProductService _productService;

        public ProductController(EcommerceAPI.Services.Interfaces.IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<EcommerceAPI.DTOs.ServiceResponse<List<ProductDto>>>> GetAllProducts()
        {
            var response = await _productService.GetAllProducts();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EcommerceAPI.DTOs.ServiceResponse<ProductDto>>> GetProduct(int id)
        {
            var response = await _productService.GetProductById(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<EcommerceAPI.DTOs.ServiceResponse<ProductDto>>> CreateProduct(CreateProductDto productDto)
        {
            var response = await _productService.CreateProduct(productDto);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        /*public async Task<ActionResult<EcommerceAPI.DTOs.ServiceResponse<ProductDto>>> UpdateProduct(int id, CreateProductDto productDto)
        {
            var response = await _productService.UpdateProduct(id, productDto);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }*/

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<EcommerceAPI.DTOs.ServiceResponse<bool>>> DeleteProduct(int id)
        {
            var response = await _productService.DeleteProduct(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
