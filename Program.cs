using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Microsoft.AspNetCore.Mvc.Infrastructure.ObjectResultExecutor>();

// Register repository with connection string from configuration or environment, fallback to localdb
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? Environment.GetEnvironmentVariable("CONNECTION_STRING")
    ?? "Server=(localdb)\\MSSQLLocalDB;Database=TrakDB;Integrated Security=True;TrustServerCertificate=True;";

builder.Services.AddSingleton<Azure.NETCoreAPI.Data.IWorkcenterTypeRepository>(sp => new Azure.NETCoreAPI.Data.WorkcenterTypeRepository(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Azure.NETCoreAPI v1");
    });
}

// Global exception handler - catches unhandled exceptions and returns JSON
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger("GlobalExceptionHandler");
        var feature = context.Features.Get<IExceptionHandlerPathFeature>();
        var ex = feature?.Error;
        logger.LogError(ex, "Unhandled exception occurred while processing request");

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var payload = new { title = "An unexpected error occurred.", detail = ex?.Message };
        await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
    });
});

// Serve static files from wwwroot (so test HTML page can be accessed)
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
