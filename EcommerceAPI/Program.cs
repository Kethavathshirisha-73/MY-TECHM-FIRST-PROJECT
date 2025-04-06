global using EcommerceAPI.Models;
global using EcommerceAPI.Services;
global using EcommerceAPI.DTOs;
global using EcommerceAPI.Helpers;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using EcommerceAPI.Data;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") // Your Angular app URL
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

// Configure Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Register Services
builder.Services.AddScoped<EcommerceAPI.Services.Interfaces.IAuthService, EcommerceAPI.Services.Implementations.AuthService>();
builder.Services.AddScoped<EcommerceAPI.Services.Interfaces.IProductService, EcommerceAPI.Services.Implementations.ProductService>();
builder.Services.AddScoped<EcommerceAPI.Services.Interfaces.ICartService, EcommerceAPI.Services.Implementations.CartService>();
builder.Services.AddScoped<EcommerceAPI.Services.Interfaces.IOrderService, EcommerceAPI.Services.Implementations.OrderService>();

// Register Repositories
builder.Services.AddScoped<EcommerceAPI.Data.Repositories.IUserRepository, EcommerceAPI.Data.Repositories.UserRepository>();
builder.Services.AddScoped<EcommerceAPI.Data.Repositories.IProductRepository, EcommerceAPI.Data.Repositories.ProductRepository>();
builder.Services.AddScoped<EcommerceAPI.Data.Repositories.ICartRepository, EcommerceAPI.Data.Repositories.CartRepository>();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "E-Commerce API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowAngular");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
