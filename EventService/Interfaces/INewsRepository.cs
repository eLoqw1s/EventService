using EventService.Models;

namespace EventService.Interfaces
{
    public interface INewsRepository
    {
        Task<List<New>> GetAllNews();
        Task<Guid> CreateNew(Guid Id, DateTime StartPublication, DateTime EndPublication, string Topic, string Text,
            string Importance, Guid AuthorId);
    }
}