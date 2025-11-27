using BookQuotes.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookQuotes.Controllers
{
    [ApiController]
    [Route( "[controller]" )]
    public class AuthorController : ControllerBase
    {
        private readonly IRepository _localRepository;

        public AuthorController( IRepository localRepository )
        {
            _localRepository = localRepository;
        }

        [HttpGet( Name = "GetAuthors" )]
        public IEnumerable<Author> GetAll( )
        {
            return _localRepository.Authors;
        }
    }
}
