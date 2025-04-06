using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EcommerceAPI.Data;
using EcommerceAPI.DTOs;
using EcommerceAPI.Models;
using EcommerceAPI.Helpers;
using EcommerceAPI.Services.Interfaces;
using AutoMapper;

namespace EcommerceAPI.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(ApplicationDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<EcommerceAPI.DTOs.ServiceResponse<string>> Register(UserRegisterDto request)
        {
            var response = new EcommerceAPI.DTOs.ServiceResponse<string>();

            try
            {
                if (await UserExists(request.Email))
                {
                    response.Success = false;
                    response.Message = "User already exists";
                    return response;
                }

                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var user = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                response.Data = CreateToken(user);
                response.Message = "Registration successful";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<EcommerceAPI.DTOs.ServiceResponse<string>> Login(UserLoginDto request)
        {
            var response = new EcommerceAPI.DTOs.ServiceResponse<string>();

            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower());

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found";
                    return response;
                }

                if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    response.Success = false;
                    response.Message = "Wrong password";
                    return response;
                }

                response.Data = CreateToken(user);
                response.Message = "Login successful";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        private async Task<bool> UserExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            return JwtHelper.CreateToken(user, _configuration["AppSettings:Token"]!);
        }
    }
}
