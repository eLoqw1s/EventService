using EventService.Exceptions;
using EventService.Interfaces;
using EventService.Models;

namespace EventService.Services
{
    public class AuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public AuthorService(IAuthorRepository authorRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _authorRepository = authorRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task Register(string name, string email, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);
            var authorEntity = new Author
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                PasswordHashed = hashedPassword
            };

            await _authorRepository.Add(authorEntity);
        }

        public async Task<string> Login(string email, string password)
        {
            var authorEntity = await _authorRepository.GetByEmail(email);

            var result = _passwordHasher.Verify(password, authorEntity.PasswordHashed);

            if (!result)
            {
                throw new CustomException("Failed login", 401);
            }

            var token = _jwtProvider.GenerateToken(authorEntity);

            return token;
        }
    }
}
