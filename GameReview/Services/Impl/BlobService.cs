
using Azure.Storage.Blobs;

namespace GameReview.Services.Impl;

public class BlobService : IBlobService
{
    private readonly BlobContainerClient _client;
    public BlobService(IConfiguration configuration)
    {
        var blobServiceClient = new BlobServiceClient(configuration["ConnectionStrings:AzureBlob"]);

        _client = blobServiceClient.GetBlobContainerClient("gamereview");
    }

    public async Task<string> UploadFile(IFormFile file)
    {
        using var fileStream = new MemoryStream();

        file.CopyTo(fileStream);
        fileStream.Position = 0;

        var blobClient = _client.GetBlobClient($"User-PP-{Guid.NewGuid()}");

        await blobClient.UploadAsync(fileStream);

        var sasURI = blobClient.GenerateSasUri(Azure.Storage.Sas.BlobSasPermissions.Read, DateTime.Now.AddYears(10));

        return sasURI.ToString();
    }
}