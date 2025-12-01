using BookQuotesRepository.Interfaces;

namespace BookQuotesRepository.Models
{
    public class Author( string? firstName, string? lastName ) : IAuthor
    {
        public string? FirstName { get; set; } = firstName;
        public string? LastName { get; set; } = lastName;
    }
}
