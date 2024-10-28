namespace GameReview.Repositories;

public interface IRepository<T> where T : class
{
    T GetById(object id);
    IEnumerable<T> GetAll();
    void Add(T entity);
    void Update(T entity);
    void Delete(object id);
}