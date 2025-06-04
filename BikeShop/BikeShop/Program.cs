using BikeShop.ServiceExtensions;
using Mapster;
using FluentValidation;
using BikeShop.Validators;
using FluentValidation.AspNetCore;
using BikeShop.DL;
using BikeShop.BL;

namespace BikeShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services
                .AddConfiguration(builder.Configuration)
                .AddDataDependencies(builder.Configuration)
                .AddBusinessDependencies();

            builder.Services.AddMapster();

            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
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

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseSwagger();
            
            app.UseSwaggerUI();

            app.MapControllers();

            app.MapHealthChecks("/healthz");

            app.Run();
        }
    }
}
