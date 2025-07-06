namespace FileManager.Api.Services;

public interface IFileService
{
	public Task<Guid> UploadAsync(IFormFile file, CancellationToken cancellationToken = default);
	public Task<IEnumerable<Guid>> UploadManyAsync(IFormFileCollection files, CancellationToken cancellationToken = default);
	public Task UploadImageAsync(IFormFile image, CancellationToken cancellationToken = default);
}
