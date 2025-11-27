using BookQuotes.Models;

namespace BookQuotes
{
    public class LocalRepository : IRepository
    {
        public Author[ ] Authors
        {
            get
            {
                var frank = new Author( "Frank", "Herbert" );
                var ray = new Author( "Ray", "Bradbury" );

                return [ frank, ray ];
            }
        }

        public Book[ ] Books
        {
            get
            {
                var bookFrank = new Book( "Dune", Authors.FirstOrDefault( author => author.FirstName == "Frank" )! );
                var bookRay = new Book( "Fahrenheit 451", Authors.FirstOrDefault( author => author.FirstName == "Ray" )! );

                return [ bookFrank, bookRay ];
            }
        }

        public Quote[ ] Quotes
        {
            get
            {
                var result = new List<Quote>( );

                var quotesFrank = new List<string>
                {
                    "I must not fear. Fear is the mind-killer. Fear is the little-death that brings total obliteration. I will face my fear. I will permit it to pass over me and through me. And when it has gone past I will turn the inner eye to see its path. Where the fear has gone there will be nothing. Only I will remain.",
                    "The mystery of life isn't a problem to solve, but a reality to experience.",
                    "A beginning is the time for taking the most delicate care that the balances are correct."
                };
                var dune = Books.FirstOrDefault( book => book.Title == "Dune" )!;
                foreach (var quoteText in quotesFrank)
                {
                    var quote = new Quote( quoteText, dune );
                    result.Add( quote );
                }

                var quotesRay = new List<string>
                {
                    "You don't have to burn books to destroy a culture. Just get people to stop reading them.",
                    "We are an impossibility in an impossible universe.",
                    "Stuff your eyes with wonder, live as if you'd drop dead in ten seconds. See the world. It's more fantastic than any dream made or paid for in factories."
                };
                var fahrenheit451 = Books.FirstOrDefault( book => book.Title == "Fahrenheit 451" )!;
                foreach (var quoteText in quotesRay)
                {
                    var quote = new Quote( quoteText, fahrenheit451 );
                    result.Add( quote );
                }

                return result.ToArray( );
            }
        }
    }
}
