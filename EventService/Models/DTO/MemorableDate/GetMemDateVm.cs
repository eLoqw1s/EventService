namespace EventService.Models.DTO.MemorableDate
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
