namespace BookQuotesRepository.Interfaces;

public interface IUser
{
    List<string> Claims { get; set; }
    string? FirstName { get; set; }
    string? LastName { get; set; }
    string Password { get; set; }
    string UserName { get; set; }
}