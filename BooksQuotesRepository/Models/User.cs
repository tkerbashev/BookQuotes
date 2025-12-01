using BookQuotesRepository.Interfaces;

namespace BookQuotesRepository.Models
{
    public class User( string userName, string password, string? firstName, string? lastName, List<string> claims ) : IUser
    {
        public string UserName { get; set; } = userName;
        public string Password { get; set; } = password;
        public string? FirstName { get; set; } = firstName;
        public string? LastName { get; set; } = lastName;
        public List<string> Claims { get; set; } = claims;
    }
}
