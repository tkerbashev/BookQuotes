namespace BookQuotes.Models
{
    public class Book( string? title, Author author )
    {
        public string? Title { get; set; } = title;
        public Author Creator { get; set; } = author;
    }
}
