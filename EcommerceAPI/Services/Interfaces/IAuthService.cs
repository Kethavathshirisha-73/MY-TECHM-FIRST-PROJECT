using EcommerceAPI.DTOs;
using EcommerceAPI.Helpers;

namespace EcommerceAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<EcommerceAPI.DTOs.ServiceResponse<string>> Register(UserRegisterDto request);
        Task<EcommerceAPI.DTOs.ServiceResponse<string>> Login(UserLoginDto request);
    }
}
