using EventService.Models;

namespace EventService.Interfaces
{
    public interface INewsRepository
    {
        Task<List<New>> GetAllNews();
        Task<Guid> CreateNew(Guid Id, DateTime StartPublication, DateTime EndPublication, string Topic, string Text,
            string Importance, Guid AuthorId);
        Task<Guid> Delete(Guid Id);
        Task<Guid> Update(Guid AuthorId, Guid Id, string Topic, string Text, string Importance, DateTime InputTime);
    }
}