using System.ComponentModel.DataAnnotations;

namespace EventService.Models.DTO
{
    public record LoginAuthorRequest
    (
        [Required] string Email,
        [Required] string Password
    );
}
