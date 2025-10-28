
using DemoAPI.Controllers;
using DemoAPI.Models;
using DemoAPI.Repositories;
using DemoAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace DemoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // сделать конфигурацию jwt
            builder.Services.Configure<JwtConfiguration>(
                builder.Configuration.GetSection("Jwt"));

            // настроить систему аутентификации
            var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtConfiguration>();
            var secretKey = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                }
                );


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<APIDBContect>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection")));

            builder.Services.AddScoped<IUserRepository, UserRepository>(); //регистрация
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddEndpointsApiExplorer();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<TestMiddleware>(); //внедрение польз middleware в конвейер запросов
            app.MapControllers();


            app.Run();
        }
    }
}
