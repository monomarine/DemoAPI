using DemoAPI.Controllers;
using DemoAPI.Models;
using DemoAPI.Repositories;
using DemoAPI.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper; 
using DemoAPI.Profiles; 
using Microsoft.Extensions.Logging; 

namespace DemoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<APIDBContect>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection")));

           
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IBookService, BookService>();

        
            LoggerFactory factory = new LoggerFactory();

            builder.Services.AddSingleton<IMapper>(_ =>
            {
                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<AuthorProfile>();
                    cfg.AddProfile<BookProfile>(); 
                    cfg.AddProfile<PostProfile>(); 
                    cfg.AddProfile<TagProfile>();  
                },
                factory);
                return configuration.CreateMapper();
            });

            var app = builder.Build();

            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseMiddleware<TestMiddleware>();
            app.MapControllers();
            app.Run();
        }
    }
}