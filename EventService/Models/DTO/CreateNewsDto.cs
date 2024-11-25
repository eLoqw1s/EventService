namespace EventService.Models.DTO
{
    public record CreateNewsDto
    (
        DateTime StartPublication,
        DateTime EndPublication,
        string Topic,
        string Text,
        string Importance
        );
}
