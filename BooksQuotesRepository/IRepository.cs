using BookQuotesRepository.Interfaces;

namespace BookQuotesRepository
{
    public interface IRepository
    {
        IAuthor[ ] Authors { get; }
        IBook[ ] Books { get; }
        IQuote[ ] Quotes { get; }
        bool ValidateUserCredentials( string? username, string? password, out IUser? user );
    }
}