using BikeShop.ServiceExtensions;

namespace BikeShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddConfiguration(builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.RegisterServices();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseSwagger();
            
            app.UseSwaggerUI();

            app.MapControllers();

            app.Run();
        }
    }
}
