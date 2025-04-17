using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WallpaperAPI.DTO;

namespace WallpaperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WallpaperCategoryController : ControllerBase
    {
        private readonly WallpaperDbContext _context;

        public WallpaperCategoryController(WallpaperDbContext context)
        {
            _context = context;
        }

        // ✅ Belirli kategoriye ait duvar kağıtlarını getir
        [HttpGet("bycategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<WallpaperDto>>> GetWallpapersByCategory(int categoryId)
        {
            var wallpapers = await _context.WallpaperCategory
                .Where(wc => wc.CategoryID == categoryId)
                .Include(wc => wc.Wallpaper)
                .Select(wc => new WallpaperDto
                {
                    WallpaperId = wc.Wallpaper.WallpaperId,
                    Title = wc.Wallpaper.Title,
                    ImageUrl = wc.Wallpaper.ImageUrl
                })
                .ToListAsync();

            return Ok(wallpapers);
        }

    }
}
