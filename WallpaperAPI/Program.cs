using Microsoft.EntityFrameworkCore;
using WallpaperAPI;

var builder = WebApplication.CreateBuilder(args);

// MySQL baðlantý dizesini alýyoruz
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WallpaperDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Servisleri konteynýra ekliyoruz
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // Tüm kaynaklardan gelen istekleri kabul et
              .AllowAnyMethod() // Tüm HTTP metodlarýna izin ver
              .AllowAnyHeader(); // Tüm baþlýklara izin ver
    });
});

var app = builder.Build();
app.UseCors("AllowAll"); // CORS politikasýný uygula

// Swagger'ý her ortamda kullan
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wallpaper API V1");
    c.RoutePrefix = string.Empty; // Swagger UI'yi kök dizine yerleþtir
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Uygulamayý baþlat
app.Run();