using System.ComponentModel.DataAnnotations;

namespace EventService.Models.DTO.Auth
{
    public record RegisterAuthorRequest
    (
        [Required] string Name,
        [Required] string Email,
        [Required] string Password
    );
}
