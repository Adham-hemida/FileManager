namespace FileManager.Api.Contracts;

public class UploadFileRequestValidator:AbstractValidator<UploadFileRequest>
{
	public UploadFileRequestValidator()
	{
		RuleFor(x => x.File)
			.Must((request, context) => request.File.Length <= FileSettings.MaxFileSizeInBytes)
			.WithMessage($"File size should be less  or equal  {FileSettings.MaxFileSizeInMB} MB.")
			.When(x => x.File is not null);

		RuleFor(x => x.File)
			.Must((request, context) =>
			{
				BinaryReader binary=new(request.File.OpenReadStream());
				var bytes = binary.ReadBytes(2);

				var fileSequenceHex = BitConverter.ToString(bytes);
				foreach (var signature in FileSettings.BlockSignatures)
				{
					if(signature.Equals(fileSequenceHex, StringComparison.OrdinalIgnoreCase))
						return false;
				}
				return true;
			})
			.WithMessage("Not allow file content")
			.When(x => x.File is not null);
	}
}

