using DemoAPI;
using DemoAPI.MappingProfiles;
using DemoAPI.Middleware;
using DemoAPI.Controllers;
using DemoAPI.Models;
using DemoAPI.Repositories;
using DemoAPI.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;


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

            builder.Services.AddSingleton<IMapper>(_ =>
            {
                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<AuthorProfile>();
                    cfg.AddProfile<BookProfile>();
                    cfg.AddProfile<PostProfile>();
                    cfg.AddProfile<TagProfile>();
                });
                return configuration.CreateMapper();
            });

            builder.Services.AddDbContext<APIDBContect>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection")));

            builder.Services.AddScoped<IUserRepository, UserRepository>(); //регистрация
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<ITagService, TagServicee>();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseAuthorization();

            app.UseMiddleware<TestMiddleware>(); //внедрение польз middleware в конвейер запросов
            app.MapControllers();

            app.Run();
        }
    }
}