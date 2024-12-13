using Microsoft.EntityFrameworkCore;
using TimeLog.Service.Factory.Interface;
using TimeLog.Service.Factory;
using TimeLog.Service.Mapper;
using TimeLog.Service.Repository.Interface;
using TimeLog.Service.Repository;
using TimeLog.Service.Service.Interface;
using TimeLog.Service.Service;
using TimeLog.Infrastructure.Context;
using System.Diagnostics;
using TimeLog.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<ITimeLogRepository, TimeLogRepository>();
builder.Services.AddScoped<ITimeLogService, TimeLogService>();
builder.Services.AddScoped<ITimeLogFactory, TimeLogFactory>();

builder.Services.AddAutoMapper(typeof(TimeLogMapperProfile));

builder.Services.AddDbContext<TimeLogContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<GlobalExceptionMiddleware>();
    var swaggerUrl = "https://localhost:7137/swagger";
    app.Lifetime.ApplicationStarted.Register(() =>
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = swaggerUrl,
            UseShellExecute = true
        });
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
