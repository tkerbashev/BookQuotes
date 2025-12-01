namespace BookQuotesRepository.Interfaces
{
    public interface IQuote
    {
        IBook Origin { get; set; }
        string? Text { get; set; }
    }
}