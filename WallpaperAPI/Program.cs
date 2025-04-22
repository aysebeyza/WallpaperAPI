using Microsoft.EntityFrameworkCore;
using WallpaperAPI;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// MySQL ba�lant�s�
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WallpaperDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Controller, Swagger, CORS
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "WallpaperAPI", Version = "v1" });

    // �retim ortam� i�in Railway URL'si
    options.AddServer(new OpenApiServer
    {
        Url = "https://wallpaperapi-production.up.railway.app"
    });

    // Geli�tirme ortam� i�in local URL
    options.AddServer(new OpenApiServer
    {
        Url = "https://localhost:7011"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Swagger her ortamda aktif
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WallpaperAPI v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
