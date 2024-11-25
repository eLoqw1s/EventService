using EventService.Models;

namespace EventService.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(Author author);
    }
}