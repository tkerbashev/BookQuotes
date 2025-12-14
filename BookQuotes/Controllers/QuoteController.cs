using BookQuotesRepository;
using BookQuotesRepository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookQuotes.Controllers
{
    [ApiController]
    [Route( "api/[controller]" )]
    public class QuoteController( IRepository localRepository ) : ControllerBase
    {
        private readonly IRepository _localRepository = localRepository;

        [HttpGet( Name = "GetQuotes" )]
        public IEnumerable<IQuote> GetAll( )
        {
            return _localRepository.Quotes;
        }
    }
}
