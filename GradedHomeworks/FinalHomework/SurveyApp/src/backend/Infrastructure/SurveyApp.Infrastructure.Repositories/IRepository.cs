namespace SurveyApp.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        Task<IList<T>> GetAllAsync();
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
