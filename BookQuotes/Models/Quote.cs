namespace BookQuotes.Models
{
    public class Quote( string? text, Book book )
    {
        public string? Text { get; set; } = text;
        public Book Origin { get; set; } = book;
    }
}
