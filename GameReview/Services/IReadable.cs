namespace GameReview.Services;

public interface IReadable<T>
{
    public T GetById(int id);
}