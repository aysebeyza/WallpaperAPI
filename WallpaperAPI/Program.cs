using Microsoft.EntityFrameworkCore;
using WallpaperAPI;

var builder = WebApplication.CreateBuilder(args);

// MySQL ba�lant� dizesini al�yoruz
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
}
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
        policy.AllowAnyOrigin() 
              .AllowAnyMethod() 
              .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("AllowAll"); 
app.UseCors("AllowAll"); // CORS politikas�n� uygula


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wallpaper API V1");
    c.RoutePrefix = string.Empty; 
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();