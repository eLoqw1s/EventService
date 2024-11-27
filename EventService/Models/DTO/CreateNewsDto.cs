using System.ComponentModel.DataAnnotations;

namespace EventService.Models.DTO
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
