using EventService.Exceptions;
using EventService.Interfaces;
using EventService.Models;
using EventService.Models.DTO.News;
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

        public async Task<List<GetNewsVm>> GetNewsByDate(DateTime selectesDate)
        {
            var newsVmEntities = await _context.News
                .Include(n => n.Author)
                .Where(news => news.StartPublication <= selectesDate && news.EndPublication >= selectesDate)
                .Select(n => new GetNewsVm(
					n.Id,
					n.StartPublication,
					n.EndPublication,
					n.Topic,
					n.Text,
					n.Importance,
					n.InputTime,
					n.Author.Name))
				.AsNoTracking()
				.ToListAsync();

			return newsVmEntities;
		}

        public async Task<List<GetNewsVm>> GetAllNews()
        {
            var newsVmEntities = await _context.News
                .Include(n => n.Author)
                .Select(n => new GetNewsVm(
                    n.Id,
                    n.StartPublication,
                    n.EndPublication,
                    n.Topic,
                    n.Text,
                    n.Importance,
                    n.InputTime,
                    n.Author.Name))
                .AsNoTracking()
                .ToListAsync();

            return newsVmEntities;
        }

        public async Task<New> GetNewById(Guid Id)
        {
            var newEntity = await _context.News
                .FirstOrDefaultAsync(news => news.Id == Id);

            if (newEntity == null)
            {
                throw new CustomException("News not found", 404);
            }

            return newEntity;
        }

        public async Task<Guid> UpdateNews(Guid Id, string Topic, string Text, 
            int Importance, DateTime StartPublication, DateTime EndPublication)
        {
            var newsEntity = await _context.News.FirstOrDefaultAsync(note => note.Id == Id);

            if (newsEntity == null)
            {
                throw new CustomException("News not found", 404);
			}

            newsEntity.Topic = Topic;
            newsEntity.Text = Text;
            newsEntity.Importance = Importance;
            newsEntity.InputTime = DateTime.Now;
            newsEntity.StartPublication = StartPublication;
            newsEntity.EndPublication = EndPublication;

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
                throw new CustomException("News not found", 404);
			}

            return Id;
        }

        public async Task<Guid> CreateNew(DateTime StartPublication, DateTime EndPublication, 
            string Topic, string Text, int Importance, Guid AuthorId)
        {
            var newsEntity = new New
            {
                Id = Guid.NewGuid(),
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
