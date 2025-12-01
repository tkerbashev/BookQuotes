using System.ComponentModel.DataAnnotations.Schema;

namespace BookQuotesRepository.DbEntities
{
    //[Table( "Users" )]
    [NotMapped]
    public class UserEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Claim { get; set; }
    }
}
