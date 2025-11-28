using BookQuotes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookQuotes.Controllers
{
    [ApiController]
    [Authorize]
    [Route( "[controller]" )]
    public class AuthorController( IRepository localRepository ) : ControllerBase
    {
        private readonly IRepository _localRepository = localRepository;

        [HttpGet( Name = "GetAuthors" )]
        public IEnumerable<Author> GetAll( )
        {
            if (User.Claims.FirstOrDefault( static c => c.Type == "list_authors" )?.Value != "true")
            {
                Response.StatusCode = 403;
                return [ ];
            }

            return _localRepository.Authors;
        }
    }
}
