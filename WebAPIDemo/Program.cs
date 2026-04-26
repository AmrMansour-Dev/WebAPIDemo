
using Microsoft.EntityFrameworkCore;
using WebAPIDemo.Models;

namespace WebAPIDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddDbContext<AppDBContext>(Options =>
Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); //Here We Injected Options to pass it to DbContext

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles(); // html,images,static files


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
