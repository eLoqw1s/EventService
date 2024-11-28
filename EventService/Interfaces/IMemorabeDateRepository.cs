using EventService.Models;
using EventService.Models.DTO.MemorableDate;

namespace EventService.Interfaces
{
    public interface IMemorabeDateRepository
    {
        Task<Guid> CreateMemDate(DateTime EventDate, string TextNotification, Guid AuthorId);
        Task<Guid> DeleteMemDate(Guid Id);
        Task<List<GetMemDateVm>> GetAllMemDate();
        Task<MemorableDate> GetMemDateById(Guid Id);
        Task<Guid> UpdateMemDate(Guid Id, DateTime EventDate, string TextNotification);
    }
}