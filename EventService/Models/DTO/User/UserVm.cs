using EventService.Models.DTO.MemorableDates;
using EventService.Models.DTO.News;

namespace EventService.Models.DTO.User
{
	public record UserVm
	(
		List<GetNewsVm> News,
		List<GetMemDateVm> MemDates
	);
}
