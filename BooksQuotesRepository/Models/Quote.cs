using BookQuotesRepository.Interfaces;

namespace BookQuotesRepository.Models
{
    public class Quote( string? text, IBook book ) : IQuote
    {
        public string? Text { get; set; } = text;
        public IBook Origin { get; set; } = book;
    }
}
