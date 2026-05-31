using BookLibraryApi.src.Application.Abstractions.DataAccess;
using BookLibraryApi.src.Application.Abstractions.DataAccess.Repositories;
using BookLibraryApi.src.Application.Abstractions.Services;
using BookLibraryApi.src.Application.Mapping;
using BookLibraryApi.src.Application.Services;
using BookLibraryApi.src.Infrastructures.DataAccess.Data;
using BookLibraryApi.src.Infrastructures.DataAccess.Repositories;
using BookLibraryApi.src.Web.Filters;
using BookLibraryApi.src.Web.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});

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

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddScoped<HttpResponseExceptionFilter>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=books.db"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            RoleClaimType = System.Security.Claims.ClaimTypes.Role
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"))
    .AddPolicy("UserOnly", policy => policy.RequireRole("User"));

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseMiddleware<RequestLoggingMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}

// Включаем обслуживание статических файлов из wwwroot
app.UseDefaultFiles(); // автоматически ищет index.html
app.UseStaticFiles();

app.MapControllers();

app.Run();