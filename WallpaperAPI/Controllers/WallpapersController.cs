using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WallpaperAPI.DTO;

namespace WallpaperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WallpapersController : ControllerBase
    {
        private readonly WallpaperDbContext _context;

        public WallpapersController(WallpaperDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WallpaperCategoryDto>>> GetAll()
        {
            try
            {
                var wallpaperCategories = await _context.WallpaperCategory
                    .Include(wc => wc.Category)
                    .Include(wc => wc.Wallpaper)
                    .Select(wc => new WallpaperCategoryDto
                    {
                        WallpaperCategoryID = wc.WallpaperCategoryID,
                        CategoryID = wc.CategoryID,
                        WallpaperID = wc.WallpaperID,
                        Category = new CategoryDto
                        {
                            CategoryID = wc.Category.CategoryID,
                            CategoryName = wc.Category.CategoryName
                        },
                        Wallpaper = new WallpaperDto
                        {
                            WallpaperId = wc.Wallpaper.WallpaperId,
                            Title = wc.Wallpaper.Title,
                            ImageUrl = wc.Wallpaper.ImageUrl
                        }
                    })
                    .ToListAsync();

                if (wallpaperCategories == null || !wallpaperCategories.Any())
                {
                    return NotFound("Hiçbir kayıt bulunamadı.");
                }

                return Ok(wallpaperCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }
    }
}
