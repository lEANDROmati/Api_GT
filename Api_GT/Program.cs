using Microsoft.EntityFrameworkCore;
using Api_GT.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TurnosContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));

var misReglasCors = "ReglasCors";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: misReglasCors, builder =>
    {
        builder.AllowAnyOrigin()//  URL localShost React
        .AllowAnyHeader()
        .AllowAnyMethod();  
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{ }
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(misReglasCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
