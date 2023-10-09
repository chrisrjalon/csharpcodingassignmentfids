using System.Reflection;
using FidsCodingAssignment.Common.Models;
using FidsCodingAssignment.Core;
using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Data;
using FidsCodingAssignment.Middlewares;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    });

    builder.Services.AddData();
    builder.Services.AddCore();
    
    builder.Services.Configure<FlightConfiguration>(
        builder.Configuration.GetSection(nameof(FlightConfiguration)));
}

var app = builder.Build();
{
    app.UseMiddleware<ExceptionHandlerMiddleware>();
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