namespace EventService.Models
{
    public class MemorableDate
    {
        public Guid Id { get; set; }
        public DateTime EventDate { get; set; }
        public string TextNotification { get; set; }
        public DateTime InputTime { get; set; }
        public Guid AuthorId { get; set; }

        public Author Author { get; set; }
    }
}
