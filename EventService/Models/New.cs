namespace EventService.Models
{
    public class New
    {
        public Guid Id { get; set; }
        public DateTime StartPublication { get; set; }
        public DateTime EndPublication { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }
        public string Importance { get; set; }
        public DateTime InputTime { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
