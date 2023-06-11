namespace KidegaApp.Services.BookService
{
    public interface IBookService
    {
        Task<IEnumerable<BookDisplayResponse>> GetAllBooksAsync();
        Task<IEnumerable<BookDisplayResponse>> GetAllBooksWithPredicateAsync(Expression<Func<Book, bool>> predicate);
        Task<BookBasketResponse> GetBookByIdAsync(int id);
        Task<IEnumerable<BookDisplayResponse>> GetBooksByCategoryAsync(int categoryId);

    }
}
