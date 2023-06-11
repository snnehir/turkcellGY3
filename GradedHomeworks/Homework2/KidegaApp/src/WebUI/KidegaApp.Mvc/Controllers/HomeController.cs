using KidegaApp.Mvc.CacheTools;
using Microsoft.Extensions.Caching.Memory;

namespace KidegaApp.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService _bookService;
        private readonly IMemoryCache _memoryCache;
        public HomeController(ILogger<HomeController> logger, IBookService bookService, IMemoryCache memoryCache)
        {
            _logger = logger;
            _bookService = bookService;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> Index(int pageNo = 1, int ? categoryId = null, string? searchTerm = "")
        {
            IEnumerable<BookDisplayResponse> books = await getBooksFromMemCachceOrDb(categoryId, searchTerm);

            var bookCount = books.Count();
            var bookPerPage = 4;

            var pagingInfo = new PagingInfo()
            {
                CurrentPage = pageNo,
                ItemsPerPage = bookPerPage,
                TotalItems = bookCount
            };

            var paginatedBooks = books.OrderBy(b => b.Id).Skip((pageNo - 1) * bookPerPage)
                                                         .Take(bookPerPage).ToList();
            ViewBag.CategoryId = categoryId;
            var model = new PaginationBookViewModel()
            {
                Books = paginatedBooks,
                PagingInfo = pagingInfo
            };
            return View(model);
        }

        private async Task<IEnumerable<BookDisplayResponse>> getBooksFromMemCachceOrDb(int? categoryId, string? searchTerm)
        {
            IEnumerable<BookDisplayResponse> books = Enumerable.Empty<BookDisplayResponse>();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                Expression<Func<Book, bool>> searchExpression = book =>
                    book.Title.ToLower().Contains(searchTerm.ToLower()) || book.Author.FirstName.ToLower().Contains(searchTerm.ToLower())
                    || book.Author.LastName.ToLower().Contains(searchTerm.ToLower());

                books = await _bookService.GetAllBooksWithPredicateAsync(searchExpression);
                ViewBag.IsSearched = true;

            }
            else
            {
                if (!_memoryCache.TryGetValue("AllBooks", out CachedBooksDataInfo cacheDataInfo))
                {
                    var options = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60))
                                                               .SetPriority(CacheItemPriority.Normal);
                    cacheDataInfo = new CachedBooksDataInfo()
                    {
                        CachedTime = DateTime.Now,
                        Books = await _bookService.GetAllBooksAsync()
                    };
                    _memoryCache.Set("AllBooks", cacheDataInfo, options);
                }
                books = categoryId == null ? cacheDataInfo.Books
                                           : await _bookService.GetBooksByCategoryAsync(categoryId.Value);
                ViewBag.IsSearched = false;
            }

            return books;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}