using BookQuotesRepository.Interfaces;

namespace BookQuotesRepository.Models
{
    public class Book( string? title, IAuthor author ) : IBook
    {
        public string? Title { get; set; } = title;
        public IAuthor Creator { get; set; } = author;
    }
}
