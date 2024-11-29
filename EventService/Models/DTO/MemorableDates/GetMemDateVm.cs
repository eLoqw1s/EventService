namespace EventService.Models.DTO.MemorableDates
{
    public record GetMemDateVm
    (
        Guid Id,
        DateTime EventDate,
        string TextNotification,
        DateTime InputTime,
        string Name
    );
}
