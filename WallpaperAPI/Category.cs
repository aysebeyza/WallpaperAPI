namespace WallpaperAPI
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string? Title { get; set; }

        public ICollection<WallpaperCategory> WallpaperCategories { get; set; }
    }
}
