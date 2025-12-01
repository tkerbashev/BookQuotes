namespace BookQuotesRepository.Interfaces
{
    public interface IBook
    {
        IAuthor Creator { get; set; }
        string? Title { get; set; }
    }
}