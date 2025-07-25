using Microsoft.EntityFrameworkCore;
using SensorAPI.BusinessLayer.Services;
using SensorAPI.DataLayer.Context;
using SensorAPI.DataLayer.Repositories.Interfaces;
using SensorAPI.DataLayer.Repositories;
using SensorAPI.BusinessLayer;
using Microsoft.OpenApi.Models;
using System.Reflection;
using SensorAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Sensor API",
        Description = "An ASP.NET Core Web API for managing Sensors and their records",
    });
    // note for the boss, I used reflection here...
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISensorRepository, SensorRepository>();
builder.Services.AddScoped<ISensorService, SensorService>();

builder.Services.AddScoped<ISensorRecordRepository, SensorRecordRepository>();
builder.Services.AddScoped<ISensorRecordService, SensorRecordService>();


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

app.UseMiddleware<ExceptionHandler>();

app.Run();
