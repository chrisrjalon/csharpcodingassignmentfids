using FidsCodingAssignment.Common.Models;
using FidsCodingAssignment.Core;
using FidsCodingAssignment.Data;
using FidsCodingAssignment.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDataServices();
builder.Services.AddCoreServices();
builder.Services.Configure<FlightConfiguration>(
    builder.Configuration.GetSection(nameof(FlightConfiguration)));

var app = builder.Build();

app.UseMiddleware<FidsExceptionHandlerMiddleware>();

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
