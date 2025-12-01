using System.ComponentModel.DataAnnotations.Schema;

namespace BookQuotesRepository.DbEntities
{
    [Table( "Authors" )]
    public class AuthorEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
