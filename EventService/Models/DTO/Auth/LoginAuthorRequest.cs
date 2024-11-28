using System.ComponentModel.DataAnnotations;

namespace EventService.Models.DTO.Auth
{
    public record LoginAuthorRequest
    (
        [Required] string Email,
        [Required] string Password
    );
}
