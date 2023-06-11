namespace KidegaApp.Mvc.CacheTools
{
    public class CachedCategoriesDataInfo
    {
        public IEnumerable<CategoryListResponse> Categories { get; set; }
        public DateTime CachedTime { get; set; }
    }
}
