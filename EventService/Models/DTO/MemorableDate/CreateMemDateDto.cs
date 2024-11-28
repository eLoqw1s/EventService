namespace EventService.Models.DTO.MemorableDate
{
	public record CreateMemDateDto
	(
		DateTime EventDate,
		string TextNotification
	);
}
