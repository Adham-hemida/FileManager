namespace FileManager.Api.Contracts;

public class UploadFileRequestValidator:AbstractValidator<UploadFileRequest>
{
	public UploadFileRequestValidator()
	{
		RuleFor(x => x.File)
			.Must((request, context) => request.File.Length <= FileSize.MaxFileSizeInBytes)
			.WithMessage($"File size should be less  or equal  {FileSize.MaxFileSizeInMB} MB.")
			.When(x => x.File is not null);
	}
}

