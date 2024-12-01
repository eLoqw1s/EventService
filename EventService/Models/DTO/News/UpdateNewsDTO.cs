namespace EventService.Models.DTO.News
{
    public record UpdateNewsDto
    (
        string Topic,
        string Text,
        int Importance,
        DateTime StartPublication,
        DateTime EndPublication
    );
}
