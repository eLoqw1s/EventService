namespace EventService.Models.DTO.MemorableDates
{
	public record UpdateMemDateDTO
	(
		DateTime EventDate,
		string TextNotification
	);
}
