using BancoChu.App;
using BancoChu.App.Extensions;
using BancoChu.Infra;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona Fluent validation
builder.Services.AddFluentValidationAutoValidation();

// Cache
builder.Services.AddMemoryCache();

// Aplica injecoes de dependencia
builder.Services.AddServices(builder.Configuration);

builder.Services.AddDbContext<BancoChuContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
