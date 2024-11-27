namespace EventService.Models.DTO
{
    public record UpdateNewsDTO
    (
        string Topic,
        string Text,
        int Importance,
        DateTime StartPublication,
        DateTime EndPublication
    );
}
