namespace EventService.Models.DTO.Error
{
	public record ErrorDto
	(
		int StatusCode,
		string Message
	);
}
