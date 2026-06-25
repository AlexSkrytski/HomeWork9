
using HomeWork9.Repositories;
using HomeWork9.Services;

namespace HomeWork9
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // =========================================================================
            // 1. РЕГИСТРАЦИЯ ЗАВИСИМОСТЕЙ (DI-КОНТЕЙНЕР)
            // =========================================================================

            // Регистрируем репозитории как Singleton, чтобы списки в памяти не удалялись
            builder.Services.AddSingleton<ProductRepository>();
            builder.Services.AddSingleton<OrderRepository>();

            // Регистрируем сервисы, которые зависят от репозиториев
            builder.Services.AddSingleton<ProductService>();
            builder.Services.AddSingleton<OrderService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
