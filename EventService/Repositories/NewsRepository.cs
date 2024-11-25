using EventService.Interfaces;
using EventService.Models;
using Microsoft.EntityFrameworkCore;

namespace EventService.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly EventServicesDbContext _context;

        public NewsRepository(EventServicesDbContext context)
        {
            _context = context;
        }

        public async Task<List<New>> GetAllNews()
        {
            var newsEntities = await _context.News
                .AsNoTracking()
                .ToListAsync();
            return newsEntities;
        }

        public async Task<Guid> Delete(Guid Id)
        {
            var newsRows = await _context.News
                .Where(b => b.Id == Id)
                .ExecuteDeleteAsync();

            if (newsRows == 0)
            {
                throw new Exception("News not found");
            }

            return Id;
        }

        public async Task<Guid> CreateNew(Guid Id, DateTime StartPublication, DateTime EndPublication, string Topic, string Text,
            string Importance, Guid AuthorId)
        {
            var newsEntity = new New
            {
                Id = Id,
                StartPublication = StartPublication,
                EndPublication = EndPublication,
                Topic = Topic,
                Text = Text,
                Importance = Importance,
                InputTime = DateTime.Now,
                AuthorId = AuthorId,
            };
            await _context.News.AddAsync(newsEntity);
            await _context.SaveChangesAsync();

            return newsEntity.Id;
        }
    }
}
