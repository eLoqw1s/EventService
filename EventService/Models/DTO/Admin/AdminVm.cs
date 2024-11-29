using EventService.Models.DTO.MemorableDates;
using EventService.Models.DTO.News;

namespace EventService.Models.DTO.Admin
{
	public record AdminVm
	(
		List<GetNewsVm> News,
		List<GetMemDateVm> MemDates
	);
}
