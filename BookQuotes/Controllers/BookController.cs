using BookQuotes.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookQuotes.Controllers
{
    [ApiController]
    [Route( "[controller]" )]
    public class BookController : ControllerBase
    {
        private readonly IRepository _localRepository;

        public BookController( IRepository localRepository )
        {
            _localRepository = localRepository;
        }

        [HttpGet( Name = "GetBooks" )]
        public IEnumerable<Book> GetAll( )
        {
            return _localRepository.Books;
        }
    }
}
