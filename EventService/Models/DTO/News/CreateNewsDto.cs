namespace EventService.Models.DTO.News
{
    public record CreateNewsDto
    (
        DateTime StartPublication,
        DateTime EndPublication,
        string Topic,
        string Text,
        int Importance
        );
}
