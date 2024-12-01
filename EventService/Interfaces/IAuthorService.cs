namespace EventService.Interfaces
{
    public interface IAuthorService
    {
        Task<string> Login(string email, string password);
        Task Register(string name, string email, string password);
    }
}