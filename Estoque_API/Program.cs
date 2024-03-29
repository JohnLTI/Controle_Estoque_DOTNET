using Estoque_API.Context;
using Microsoft.EntityFrameworkCore;
using Estoque_API.Services;
using MySqlConnector;
using Estoque_API.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Pega a string de conexao
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Configura o contexto 
builder.Services.AddDbContext<EstoqueDbContext>(options =>
        options.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<IServiceProduto, ServiceProduto>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();