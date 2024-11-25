using EventService.Interfaces;
using EventService.Models;
using Microsoft.EntityFrameworkCore;

namespace EventService.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly EventServicesDbContext _context;

        public AuthorRepository(EventServicesDbContext context)
        {
            _context = context;
        }

        public async Task Add(Author author)
        {
            var authorHaveAccountCheck = await _context.Authors
                .AsNoTracking()
                .FirstOrDefaultAsync(auth => auth.Email == author.Email);

            if (authorHaveAccountCheck != null)
            {
                throw new Exception("The author already exists");
            }

            var authorEntity = new Author
            {
                Id = author.Id,
                Name = author.Name,
                Email = author.Email,
                PasswordHashed = author.PasswordHashed
            };

            await _context.Authors.AddAsync(authorEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<Author> GetByEmail(string email)
        {
            var authorEntity = await _context.Authors
                .AsNoTracking()
                .FirstOrDefaultAsync(author => author.Email == email);

            if (authorEntity == null)
            {
                throw new Exception("Failed email");
            }

            return authorEntity;
        }
    }
}
