namespace KidegaApp.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        Task<T?> GetByIdAsync(int id);
        Task<IList<T>> GetAllAsync();
        Task<IList<T>> GetAllWithPredicateAsync(Expression<Func<T, bool>> predicate);

        Task CreateAsync(T entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(T entity);
    }
}