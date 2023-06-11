namespace KidegaApp.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<IEnumerable<BookDisplayResponse>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return books.Adapt<IEnumerable<BookDisplayResponse>>();

        }

        public async Task<IEnumerable<BookDisplayResponse>> GetBooksByCategoryAsync(int categoryId)
        {
            var books = await _bookRepository.GetBooksByCategoryAsync(categoryId);
            return books.Adapt<IEnumerable<BookDisplayResponse>>();
        }

        public async Task<IEnumerable<BookDisplayResponse>> GetAllBooksWithPredicateAsync(Expression<Func<Book, bool>> predicate)
        {
            var books = await _bookRepository.GetAllWithPredicateAsync(predicate);
            if (!books.Any())
            {
                return Enumerable.Empty<BookDisplayResponse>();
            }
            return books.Adapt<IEnumerable<BookDisplayResponse>>();
        }

        public async Task<BookBasketResponse> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return book.Adapt<BookBasketResponse>();

        }
    }
}
