namespace FileManager.Api.Services;

public interface IFileService
{
	public Task<Guid> UploadAsync(IFormFile file, CancellationToken cancellationToken = default);
}
