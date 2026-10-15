using Microsoft.AspNetCore.Components.Routing;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ToDo.Api.Data;
using ToDo.Api.Endpoints;
using ToDo.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(
    builder.Configuration
    .GetConnectionString("DefaultConnection"))
);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
//ESTO SE AGREGO AL REALIZAR EL CAMBIO
app.MapToDoItemsEndpoints();

app.Run();
