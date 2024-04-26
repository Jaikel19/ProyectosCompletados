using AppLogic;
using DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<MiMongoDB>(_ => new MiMongoDB());
builder.Services.AddScoped<CelularManager, CelularManager>();
builder.Services.AddScoped<ComputadoraManager, ComputadoraManager>();
builder.Services.AddScoped<ImpresoraManager, ImpresoraManager>();
builder.Services.AddScoped<DepartamentoManager, DepartamentoManager>();
builder.Services.AddScoped<UsuarioManager, UsuarioManager>();


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


