var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Microsoft.AspNetCore.Mvc.Infrastructure.ObjectResultExecutor>();

// Register repository with connection string from configuration
var connectionString = "Data Source=10.16.27.14;Database=TrakDB;Integrated Security=False;User Id=WebAppUser;Password=CWRP@2021!;MultipleActiveResultSets=True;persist security info=True;TrustServerCertificate=True;";
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

app.UseAuthorization();

app.MapControllers();

app.Run();
