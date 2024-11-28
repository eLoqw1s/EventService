namespace EventService.Models.DTO.News
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
