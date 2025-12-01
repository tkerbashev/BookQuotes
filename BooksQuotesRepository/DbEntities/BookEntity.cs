using System.ComponentModel.DataAnnotations.Schema;

namespace BookQuotesRepository.DbEntities
{
    //[Table( "Books" )]
    public class BookEntity
    {
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
