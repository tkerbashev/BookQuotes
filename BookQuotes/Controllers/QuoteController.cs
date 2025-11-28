using BookQuotes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookQuotes.Controllers
{
    [ApiController]
    [Authorize]
    [Route( "[controller]" )]
    public class QuoteController( IRepository localRepository ) : ControllerBase
    {
        private readonly IRepository _localRepository = localRepository;

        [HttpGet( Name = "GetQuotes" )]
        public IEnumerable<Quote> GetAll( )
        {
            return _localRepository.Quotes;
        }
    }
}
