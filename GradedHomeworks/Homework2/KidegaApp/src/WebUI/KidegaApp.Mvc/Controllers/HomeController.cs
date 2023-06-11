using KidegaApp.Services.BookService;

namespace KidegaApp.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService _bookService;
        public HomeController(ILogger<HomeController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        public async Task<IActionResult> Index(int pageNo = 1, int ? categoryId = null)
        {
            var books = categoryId == null ? await _bookService.GetAllBooksAsync()
                                           : await _bookService.GetBooksByCategoryAsync(categoryId.Value);

            var bookCount = books.Count();
            var bookPerPage = 4;

            var pagingInfo = new PagingInfo()
            {
                CurrentPage = pageNo,
                ItemsPerPage = bookPerPage,
                TotalItems = bookCount
            };

            var paginatedBooks = books.OrderBy(b => b.Id).Skip((pageNo-1)*bookPerPage)
                                                         .Take(bookPerPage).ToList();        

            var model = new PaginationBookViewModel()
            {
                PagingInfo = pagingInfo,
                Books = paginatedBooks,
            };
            return View(model);
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