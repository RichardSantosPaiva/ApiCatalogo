using System.Text;
using System.Text.Json.Serialization;
using ApiCatalogo.Context;
using ApiCatalogo.Extensions;
using ApiCatalogo.Filters;
using ApiCatalogo.Logging;
using ApiCatalogo.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

string mysqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mysqlConnection,
    ServerVersion.AutoDetect(mysqlConnection)));

builder.Services.AddScoped<ApiLogginFilters>();

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFiler));
}).AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var app = builder.Build();

app.UseCors("PermitirTudo");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
