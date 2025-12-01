using BookQuotesRepository.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace BookQuotesRepository
{
    internal class BookQuotesDbContext( DbContextOptions<BookQuotesDbContext> options ) : DbContext( options )
    {
        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.Entity<AuthorEntity>( ).HasNoKey( );
            modelBuilder.Entity<BookEntity>( ).HasNoKey( );
            modelBuilder.Entity<QuoteEntity>( ).HasNoKey( );
            modelBuilder.Entity<UserEntity>( ).HasNoKey( );
        }

        public DbSet<AuthorEntity> AuthorEntities { get; set; }

        public DbSet<BookEntity> BookEntities { get; set; }

        public DbSet<QuoteEntity> QuoteEntities { get; set; }

        public DbSet<UserEntity> UserEntities { get; set; }
    }
}
