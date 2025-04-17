namespace WallpaperAPI
{
    public class WallpaperCategory
    {
          public int WallpaperCategoryID { get; set; }

            public int CategoryID { get; set; }
            public virtual  Category Category { get; set; }

            public int WallpaperID { get; set; }
            public virtual Wallpaper Wallpaper { get; set; }
        
    }
}
