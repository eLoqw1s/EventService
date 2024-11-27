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

        public async Task<New> GetNewById(Guid Id)
        {
            var newEntity = await _context.News
                .FirstOrDefaultAsync(news => news.Id == Id);

            if (newEntity == null)
            {
                throw new Exception("New not found");
            }

            return newEntity;
        }

        public async Task<Guid> UpdateNews(Guid Id, string Topic, string Text, int Importance, DateTime InputTime)
        {
            var newsEntity = await _context.News.FirstOrDefaultAsync(note => note.Id == Id);

            if (newsEntity == null)
            {
                throw new Exception("news not found");
            }

            newsEntity.Topic = Topic;
            newsEntity.Text = Text;
            newsEntity.Importance = Importance;
            newsEntity.InputTime = InputTime;

            await _context.SaveChangesAsync();

            return newsEntity.Id;
        }


        public async Task<Guid> DeleteNews(Guid Id)
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
            int Importance, Guid AuthorId)
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
