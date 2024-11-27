using EventService.Models;

namespace EventService.Interfaces
{
    public interface INewsRepository
    {
        Task<List<New>> GetAllNews();
        Task<Guid> CreateNew(Guid Id, DateTime StartPublication, DateTime EndPublication, string Topic, string Text,
            int Importance, Guid AuthorId);
        Task<Guid> DeleteNews(Guid Id);
        Task<Guid> UpdateNews(Guid Id, string Topic, string Text, int Importance, DateTime InputTime);
        Task<New> GetNewById(Guid Id);
    }
}