namespace WallpaperAPI
{
    public class Wallpaper
    {
        public int WallpaperId { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }

        public ICollection<WallpaperCategory> WallpaperCategories { get; set; }
    }
}
