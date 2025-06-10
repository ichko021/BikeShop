using BikeShop.ServiceExtensions;
using Mapster;
using FluentValidation;
using BikeShop.Validators;
using FluentValidation.AspNetCore;
using BikeShop.DL;
using BikeShop.BL;
using BikeShop.MapsterConfig;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddConfiguration(builder.Configuration)
    .AddDataDependencies(builder.Configuration)
    .AddBusinessDependencies();

builder.Services.AddMapster();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
MapsterConfig.Configure();
builder.Services.AddValidatorsFromAssemblyContaining<AddBikeRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AddPartRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddHealthChecks();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.MapHealthChecks("/healthz");

app.Run();