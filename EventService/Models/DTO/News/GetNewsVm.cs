namespace EventService.Models.DTO.News
{
    public record GetNewsVm
    (
        Guid Id,
        DateTime StartPublication,
        DateTime EndPublication,
        string Topic,
        string Text,
        int Importance,
        DateTime InputTime,
        string AuthorName
    );
}
