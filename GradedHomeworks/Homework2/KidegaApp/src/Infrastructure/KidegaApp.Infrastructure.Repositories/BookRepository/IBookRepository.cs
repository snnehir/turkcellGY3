namespace KidegaApp.Infrastructure.Repositories.BookRepository
{
    public interface IBookRepository: IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId);
    }
}
