namespace EventService.Models.DTO.MemorableDate
{
	public record UpdateMemDateDTO
	(
		DateTime EventDate,
		string TextNotification
	);
}
