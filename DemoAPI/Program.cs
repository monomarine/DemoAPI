
using AutoMapper;
using DemoAPI.Controllers;
using DemoAPI.Models;
using DemoAPI.Repositories;
using DemoAPI.Services;
using Microsoft.EntityFrameworkCore;


namespace DemoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<APIDBContect>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection")));
            ILoggerFactory loggerFactory = new LoggerFactory();
            builder.Services.AddSingleton<IMapper>(_ =>
            {
                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<AuthorProfile>();
                });

                return configuration.CreateMapper();
            });
            builder.Services.AddScoped<IUserRepository, UserRepository>(); //регистрация
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<ITagService, TagServicee>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<TestMiddleware>(); //внедрение польз middleware в конвейер запросов
            app.MapControllers();

            app.Run();
        }
    }
}
