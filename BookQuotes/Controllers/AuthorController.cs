using BookQuotesRepository;
using BookQuotesRepository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookQuotes.Controllers
{
    [ApiController]
    [Authorize]
    [Route( "api/[controller]" )]
    public class AuthorController( IRepository localRepository ) : ControllerBase
    {
        private readonly IRepository _localRepository = localRepository;

        [HttpGet( Name = "GetAuthors" )]
        public IEnumerable<IAuthor> GetAll( )
        {
            // Food for thought: Do we really need to check for "true"?
            if (User.Claims.FirstOrDefault( static c => c.Type == "list_authors" )?.Value != "true")
            {
                Response.StatusCode = 403;
                return [ ];
            }

            return _localRepository.Authors;
        }
    }
}
