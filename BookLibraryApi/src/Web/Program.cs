using BookLibraryApi.src.Application.Abstractions;
using BookLibraryApi.src.Application.Mapping;
using BookLibraryApi.src.Application.Services;
using BookLibraryApi.src.Infrastructures.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

// Настройка CORS (Тестовый пример, в реальных случаях так делать нельзя!)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IBooksService, BooksService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=books.db"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

// Включаем обслуживание статических файлов из wwwroot
app.UseDefaultFiles(); // автоматически ищет index.html
app.UseStaticFiles();

app.MapControllers();

app.Run();