namespace KidegaApp.Mvc.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly IBookService _bookService;
        public ShoppingController(IBookService bookService)
        {
            this._bookService = bookService;
        }
        public IActionResult Index()
        {
            var collection = getBookCollectionFromSession();
            return View(collection);
        }
        
        public async Task<IActionResult> AddBook(int id)
        {
            BookBasketResponse selectedBook = await _bookService.GetBookByIdAsync(id);
            var bookItem = new BookItem()
            {
                Book = selectedBook,
                Quantity = 1
            };

            BookCollection bookCollection = getBookCollectionFromSession();
            bookCollection.AddNewBook(bookItem);
            saveToSession(bookCollection);

            return Json(new { message = $"Book with id {id} is added to your cart." });
        }

        public IActionResult IncreaseBookQuantity(int id)
        {
            // get collection
            BookCollection bookCollection = getBookCollectionFromSession();
            if(bookCollection != null)
            {
                var item = bookCollection.BookItems.Any(p=>p.Book.Id == id);
                if(item)
                {
                    bookCollection.BookItems.FirstOrDefault(p => p.Book.Id == id).Quantity += 1;
                    saveToSession(bookCollection);
                }
                return Json(new { message = $"Number of book with id {id} is increased." });
            }
            return Json(new { message = $"Book with id {id} is not found" });
        }

        public IActionResult DecreaseBookQuantity(int id)
        {
            BookCollection bookCollection = getBookCollectionFromSession();
            if (bookCollection != null)
            {
                var exists = bookCollection.BookItems.Any(p => p.Book.Id == id);
                if (exists)
                {
                    var item = bookCollection.BookItems.FirstOrDefault(p => p.Book.Id == id);
                    if (item.Quantity == 1)
                    {
                        bookCollection.BookItems.Remove(item);
                        saveToSession(bookCollection);
                        return Json(new { message = $"Rlant with id {id} is removed." });
                    }
                    else
                    {
                        bookCollection.BookItems.FirstOrDefault(p => p.Book.Id == id).Quantity -= 1;
                        saveToSession(bookCollection);
                        return Json(new { message = $"Number of book with id {id} is decreased." });
                    }
                    
                }
                
            }
            return Json(new { message = $"Book with id {id} is not found" });
        }

        private BookCollection getBookCollectionFromSession()
        {
            return HttpContext.Session.GetJson<BookCollection>("basket") ?? new BookCollection();
        }


        private void saveToSession(BookCollection courseCollection)
        {

            HttpContext.Session.SetJson("basket", courseCollection);

        }
        
       
    }
}
