using KidegaApp.Mvc.CacheTools;
using Microsoft.Extensions.Caching.Memory;

namespace KidegaApp.Mvc.ViewComponents
{
    public class MenuViewComponent: ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IMemoryCache _memoryCache;
        public MenuViewComponent(ICategoryService categoryService, IMemoryCache memoryCache)
        {
            _categoryService = categoryService;
            _memoryCache = memoryCache;
        }
        public IViewComponentResult Invoke()
        {
            if (!_memoryCache.TryGetValue("Categories", out CachedCategoriesDataInfo cacheDataInfo))
            {
                var options = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60))
                                                           .SetPriority(CacheItemPriority.Normal);
                cacheDataInfo = new CachedCategoriesDataInfo()
                {
                    CachedTime = DateTime.Now,
                    Categories = _categoryService.GetCategoryList()
            };
                _memoryCache.Set("Categories", cacheDataInfo, options);
            }
            return View(cacheDataInfo.Categories);
        }
    }
}
