namespace GameReview.Services;

public interface IEditable<T, DTO>
{
    public T Update(int id, DTO dto);
}