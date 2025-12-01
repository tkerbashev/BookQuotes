using System.ComponentModel.DataAnnotations.Schema;

namespace BookQuotesRepository.DbEntities
{
    public class QuoteEntity
    {
        public string? Text { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
