namespace EventService.Models.DTO.MemorableDates
{
	public record CreateMemDateDto
	(
		DateTime EventDate,
		string TextNotification
	);
}
