namespace EventService.Models.DTO
{
    public record UpdateNewsDTO
    (
        Guid Id,
        string Topic,
        string Text,
        string Importance
    );
}
