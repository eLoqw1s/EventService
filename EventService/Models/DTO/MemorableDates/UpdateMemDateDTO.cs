namespace EventService.Models.DTO.MemorableDates
{
	public record UpdateMemDateDto
	(
		DateTime EventDate,
		string TextNotification
	);
}
