using BookQuotesRepository.DbEntities;
using BookQuotesRepository.Interfaces;
using BookQuotesRepository.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BookQuotesRepository
{
    public class DbRepository : IRepository
    {
        // TODO: retrieve from configuration
        private readonly string dbPath = Path.Combine
            (
            Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly( ).Location )!,
            "Database",
            "BookQuotes.db"
            );

        public IAuthor[ ] Authors
        {
            get
            {
                using var connection = new SqliteConnection( "Data Source=" + dbPath );
                connection.Open( );

                DbContextOptions<BookQuotesDbContext> options = new DbContextOptionsBuilder<BookQuotesDbContext>( )
                    .UseSqlite( connection )
                    .Options;

                var context = new BookQuotesDbContext( options );

                var authors = context.AuthorEntities.Select( entity => new Author( entity.FirstName!, entity.LastName! ) );
                return [ .. authors ];
            }
        }

        public IBook[ ] Books
        {
            get
            {
                using var connection = new SqliteConnection( "Data Source=" + dbPath );
                connection.Open( );

                DbContextOptions<BookQuotesDbContext> options = new DbContextOptionsBuilder<BookQuotesDbContext>( )
                    .UseSqlite( connection )
                    .Options;

                var context = new BookQuotesDbContext( options );

                List<BookEntity> bookEntities = [ .. context.BookEntities.FromSqlRaw(
                $@"SELECT Title, FirstName, LastName
                FROM Books
                INNER JOIN Authors ON Books.Id = Authors.Id"
                ) ];

                var books = bookEntities.Select( entity => new Book( entity.Title, new Author( entity.FirstName!, entity.LastName! ) ) );
                return [ .. books ];
            }
        }

        public IQuote[ ] Quotes
        {
            get
            {
                using var connection = new SqliteConnection( "Data Source=" + dbPath );
                connection.Open( );

                DbContextOptions<BookQuotesDbContext> options = new DbContextOptionsBuilder<BookQuotesDbContext>( )
                    .UseSqlite( connection )
                    .Options;

                var context = new BookQuotesDbContext( options );

                List<QuoteEntity> quoteEntities = [ .. context.QuoteEntities.FromSqlRaw(
                $@"SELECT Text, Title, FirstName, LastName
                FROM Quotes
                INNER JOIN Books ON Quotes.Book = Books.Id
                INNER JOIN Authors ON Books.Author = Authors.Id"
                ) ];

                var quotes = quoteEntities.Select( entity => new Quote( entity.Text, new Book( entity.Title, new Author( entity.FirstName!, entity.LastName! ) ) ) );
                return [ .. quotes ];
            }
        }

        public bool ValidateUserCredentials( string? username, string? password, out IUser? user )
        {
            if (string.IsNullOrWhiteSpace( username ) || string.IsNullOrWhiteSpace( password ) )
            {
                user = null;
                return false;
            }

            using var connection = new SqliteConnection( "Data Source=" + dbPath );
            connection.Open( );

            DbContextOptions<BookQuotesDbContext> options = new DbContextOptionsBuilder<BookQuotesDbContext>( )
                .UseSqlite( connection )
                .Options;

            var context = new BookQuotesDbContext( options );

            List<UserEntity> users = [ .. context.UserEntities.FromSqlRaw(
                $@"SELECT FirstName, LastName, Claim
                FROM Users
                LEFT OUTER JOIN Claims ON Users.Username = Claims.Username
                WHERE Users.Username = @username AND Users.Password = @password",
                new SqliteParameter( "username", username ),
                new SqliteParameter( "password", password )) ];

            if ( users.Count > 0)
            {
                var firstUser = users[0];
                user = new User
                ( username, password, firstUser.FirstName, firstUser.LastName,
                    [ .. users.Where( u => u.Claim is not null ).Select( u => u.Claim! ) ] );
                return true;
            }
            else
            {
                user = null;
                return false;
            }
        }
    }
}
