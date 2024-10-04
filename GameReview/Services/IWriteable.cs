namespace GameReview.Services;

public interface IWriteable<T, DTO>
{
    public T Create(DTO dto);
}