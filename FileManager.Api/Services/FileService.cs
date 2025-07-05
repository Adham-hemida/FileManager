
namespace FileManager.Api.Services;

public class FileService(IWebHostEnvironment webHostEnvironment,ApplicationDbContext context) : IFileService
{
	private readonly string _filePath = $"{webHostEnvironment.WebRootPath}/uploads";
	private readonly ApplicationDbContext _context = context;

	public async Task<Guid> UploadAsync(IFormFile file, CancellationToken cancellationToken = default)
	{
		var randomFileName =Path.GetRandomFileName();

		var uploadedFile = new UploadedFile
		{
			FileName = file.FileName,
			StoredFileName = randomFileName,
			ContentType = file.ContentType,
			FileExtension = Path.GetExtension(file.FileName)
		};

		var path=Path.Combine(_filePath,randomFileName);

		using var stream = File.Create(path);
		await file.CopyToAsync(stream, cancellationToken);

		await _context.AddAsync(uploadedFile, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

		return uploadedFile.Id;
	}
}
