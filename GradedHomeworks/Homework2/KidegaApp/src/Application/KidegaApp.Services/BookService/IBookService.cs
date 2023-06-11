namespace KidegaApp.Services.BookService
{
    public interface IBookService
    {
        Task<IEnumerable<BookDisplayResponse>> GetAllBooksAsync();
        Task<IEnumerable<BookDisplayResponse>> GetBooksByCategoryAsync(int categoryId);

    }
}
