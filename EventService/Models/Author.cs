namespace EventService.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHashed { get; set; }
    }
}
