using Finora.Application;
using Finora.Application.Common.Models;
using Finora.Application.Common.Settings;
using Finora.Infrastructure.Extensions;
using Finora.Middleware;
using Finora.Persistence.Context;
using Finora.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => 
{ 
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<ApplicationDbSeeder>();
    await seeder.SeedAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AngularPolicy");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
