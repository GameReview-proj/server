namespace GameReview.Services;

public interface IBlobService
{
    public Task<string> UploadFile(IFormFile file);
}