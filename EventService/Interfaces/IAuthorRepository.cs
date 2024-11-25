using EventService.Models;

namespace EventService.Interfaces
{
    public interface IAuthorRepository
    {
        Task Add(Author author);
        Task<Author> GetByEmail(string email);
    }
}