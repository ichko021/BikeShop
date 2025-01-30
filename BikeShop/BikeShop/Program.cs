using BikeShop.ServiceExtensions;
using Mapster;
using FluentValidation;
using BikeShop.Validators;
using FluentValidation.AspNetCore;

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
            builder.Services.AddMapster();
            MapsterConfig.MapsterConfig.Configure();
            builder.Services.AddValidatorsFromAssemblyContaining<AddBikeRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<AddPartRequestValidator>();
            builder.Services.AddFluentValidationAutoValidation();
            //builder.Services.AddHealthChecks()
            //    .AddCheck<Healthcheck>("Sample");
            builder.Services.AddHealthChecks();


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseSwagger();
            
            app.UseSwaggerUI();

            app.MapControllers();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.MapHealthChecks("/healthz");

            app.Run();
        }
    }
}
