

namespace FileManager.Api.Services;

public class FileService(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context) : IFileService
{
	private readonly string _filePath = $"{webHostEnvironment.WebRootPath}/uploads";
	private readonly string _imagePath = $"{webHostEnvironment.WebRootPath}/images";
	private readonly ApplicationDbContext _context = context;

	public async Task<Guid> UploadAsync(IFormFile file, CancellationToken cancellationToken = default)
	{
		var uploadedFile = await SaveFile(file, cancellationToken);

		await _context.AddAsync(uploadedFile, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

		return uploadedFile.Id;
	}

	public async Task<IEnumerable<Guid>> UploadManyAsync(IFormFileCollection files, CancellationToken cancellationToken = default)
	{
		List<UploadedFile> uploadedFiles = [];
		foreach (var file in files)
		{
			var uploadedFile = await SaveFile(file, cancellationToken);
			uploadedFiles.Add(uploadedFile);
		}
		await _context.AddRangeAsync(uploadedFiles, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

		return uploadedFiles.Select(x => x.Id).ToList();
	}

	public async Task UploadImageAsync(IFormFile image, CancellationToken cancellationToken = default)
	{
		var path = Path.Combine(_imagePath, image.FileName);

		using var stream = File.Create(path);
		await image.CopyToAsync(stream, cancellationToken);
	}

	public async Task<(byte[] fileContent, string contentType, string fileName)> DownLoadAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var file = await _context.Files.FindAsync(id, cancellationToken);
		if (file == null)
			return ([], string.Empty, string.Empty);

		var path = Path.Combine(_filePath, file.StoredFileName);
		MemoryStream memoryStream = new();
		using FileStream fileStream = new (path, FileMode.Open);
	    fileStream.CopyTo(memoryStream);
		memoryStream.Position = 0; // Reset the position to the beginning of the stream
		return (memoryStream.ToArray(), file.ContentType, file.FileName);

	}
	private async Task<UploadedFile> SaveFile(IFormFile file, CancellationToken cancellationToken = default)
	{
		var randomFileName = Path.GetRandomFileName();

		var uploadedFile = new UploadedFile
		{
			FileName = file.FileName,
			StoredFileName = randomFileName,
			ContentType = file.ContentType,
			FileExtension = Path.GetExtension(file.FileName)
		};

		var path = Path.Combine(_filePath, randomFileName);

		using var stream = File.Create(path);
		await file.CopyToAsync(stream, cancellationToken);

		return uploadedFile;
	}


}
