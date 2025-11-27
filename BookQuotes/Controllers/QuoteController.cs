using BookQuotes.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookQuotes.Controllers
{
    [ApiController]
    [Route( "[controller]" )]
    public class QuoteController : ControllerBase
    {
        private readonly IRepository _localRepository;

        public QuoteController( IRepository localRepository )
        {
            _localRepository = localRepository;
        }

        [HttpGet( Name = "GetQuotes" )]
        public IEnumerable<Quote> GetAll( )
        {
            return _localRepository.Quotes;
        }
    }
}
