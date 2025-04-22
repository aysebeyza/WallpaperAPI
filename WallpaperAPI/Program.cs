using Microsoft.EntityFrameworkCore;
using WallpaperAPI;

var builder = WebApplication.CreateBuilder(args);

// MySQL ba�lant� dizesini al�yoruz
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WallpaperDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Servisleri konteyn�ra ekliyoruz
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // T�m kaynaklardan gelen istekleri kabul et
              .AllowAnyMethod() // T�m HTTP metodlar�na izin ver
              .AllowAnyHeader(); // T�m ba�l�klara izin ver
    });
});

var app = builder.Build();
app.UseCors("AllowAll"); // CORS politikas�n� uygula

// Swagger'� her ortamda kullan
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wallpaper API V1");
    c.RoutePrefix = string.Empty; // Swagger UI'yi k�k dizine yerle�tir
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Uygulamay� ba�lat
app.Run();