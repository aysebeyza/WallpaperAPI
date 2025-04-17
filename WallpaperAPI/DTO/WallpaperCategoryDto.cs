using WallpaperAPI;

namespace WallpaperAPI.DTO
{
    public class WallpaperCategoryDto
    {
        public int WallpaperCategoryID { get; set; }
        public int CategoryID { get; set; }
        public int WallpaperID { get; set; }

        public CategoryDto Category { get; set; }
        public WallpaperDto Wallpaper { get; set; }
    }
}
