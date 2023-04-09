public interface IBaseService<T>
{
    void Add(T entity);
    List<T> GetAll();
    T? GetById(int Id);
    void Update(T entity);
    void Remove(int Id);
}