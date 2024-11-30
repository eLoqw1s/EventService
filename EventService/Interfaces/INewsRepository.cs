using EventService.Models;
using EventService.Models.DTO.News;

namespace EventService.Interfaces
{
    public interface INewsRepository
    {
        Task<List<GetNewsVm>> GetNewsByDate(DateTime selectesDate);
		Task<List<GetNewsVm>> GetAllNews();
        Task<Guid> CreateNew(DateTime StartPublication, DateTime EndPublication, string Topic, string Text,
            int Importance, Guid AuthorId);
        Task<Guid> DeleteNews(Guid Id);
        Task<Guid> UpdateNews(Guid Id, string Topic, string Text,
            int Importance, DateTime StartPublication, DateTime EndPublication);
        Task<New> GetNewById(Guid Id);
    }
}