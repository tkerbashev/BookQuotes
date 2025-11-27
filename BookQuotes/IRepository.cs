using BookQuotes.Models;

namespace BookQuotes
{
    public interface IRepository
    {
        Author[ ] Authors { get; }
        Book[ ] Books { get; }
        Quote[ ] Quotes { get; }
    }
}