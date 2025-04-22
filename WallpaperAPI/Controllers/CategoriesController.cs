using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WallpaperAPI.DTO;

namespace WallpaperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly WallpaperDbContext _context;

        public CategoriesController(WallpaperDbContext context)
        {
            _context = context;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            try
            {
                var categories = await _context.Category
                    .Select(c => new CategoryDto
                    {
                        CategoryID = c.CategoryID,
                        CategoryName = c.CategoryName
                    })
                    .ToListAsync();

                if (categories == null || !categories.Any())
                {
                    return NotFound("Kategori bulunamadı.");
                }

                return Ok(categories);
              
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }
    }
}
