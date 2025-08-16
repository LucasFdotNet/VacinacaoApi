using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VacinacaoApi.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<VacinacaoContext>(options =>
    options.UseSqlServer(connectionString));

// 2. FluentValidation
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// 3. MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 4. Swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();