using BookQuotes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookQuotes.Controllers
{
    [ApiController]
    [Authorize]
    [Route( "[controller]" )]
    public class BookController( IRepository localRepository ) : ControllerBase
    {
        private readonly IRepository _localRepository = localRepository;

        [HttpGet( Name = "GetBooks" )]
        public IEnumerable<Book> GetAll( )
        {
            return _localRepository.Books;
        }
    }
}
