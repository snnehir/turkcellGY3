namespace KidegaApp.Mvc.CacheTools
{
    public class CachedBooksDataInfo
    {
        public IEnumerable<BookDisplayResponse> Books { get; set; }
        public DateTime CachedTime { get; set; }

    }
}
