
namespace FileManager.Api.Contracts;

public class UploadFileRequestValidator:AbstractValidator<UploadFileRequest>
{
	public UploadFileRequestValidator()
	{
		RuleFor(x => x.File)
			.SetValidator(new FileSizeValidator());

		RuleFor(x => x.File)
			.SetValidator(new BlockedSignatureValidator());

		RuleFor(x => x.File)
			.SetValidator(new FileNameValidator());
	}
}

