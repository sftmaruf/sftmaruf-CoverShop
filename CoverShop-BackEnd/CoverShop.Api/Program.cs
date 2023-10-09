using CoverShop.Api.Settings;
using CoverShop.Application;
using CoverShop.Infrastructure;
using Serilog;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder
    .AddAutofac()
    .AddSerilog()
    .AddApi()
    .AddSwagger();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

try
{
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch(Exception ex)
{
    Log.Fatal("The app is crushed", ex);
}
finally
{
    Log.CloseAndFlush();
}