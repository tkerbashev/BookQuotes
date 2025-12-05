using BookQuotesRepository;
using BookQuotesRepository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookQuotes.Controllers
{
    [ApiController]
    [Route( "api/[controller]" )]
    public class AuthenticationController( IConfiguration configuration, IRepository localRepository ) : ControllerBase
    {
        private readonly IRepository _repository = localRepository;

        public record AuthenticationRequestBody( string? Username, string? Password );

        private record UserInfo( string Username, string FirstName, string LastName, List<string> Claims );

        private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException( nameof( configuration ) );

        [HttpPost("authenticate")]
        public IActionResult Authenticate( AuthenticationRequestBody authenticationRequestBody )
        {
            // Step 1: Validate the user credentials
            var user = ValidateUserCredentials( authenticationRequestBody.Username, authenticationRequestBody.Password );

            if ( user is null )
            {
                return Unauthorized( new { Message = "Invalid username or password." } );
            }

            // Step 2: Generate a JWT token
            var securityKey = new SymmetricSecurityKey( Convert.FromBase64String( _configuration[ "Authentication:SecretForKey" ] ?? throw new InvalidOperationException( "JWT Key not configured." ) ) );
            var signingCredentials = new SigningCredentials( securityKey, SecurityAlgorithms.HmacSha256 );

            var claimsForToken = new List<Claim>();
            foreach ( var userClaim in user.Claims )
            {
                claimsForToken.Add( new Claim( userClaim, "true" ) );
            }

            var jwtSecurityToken = new JwtSecurityToken
            (
                _configuration[ "Authentication:Issuer" ],
                _configuration[ "Authentication:Audience" ],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours( 1 ),
                signingCredentials
            );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken( jwtSecurityToken );

            return Ok( new { Token = tokenToReturn } );
        }

        private UserInfo? ValidateUserCredentials( string? username, string? password )
        {
            // Check the provided username and password against your user store (e.g., database)
            if ( _repository.ValidateUserCredentials( username, password, out IUser? user))
            {
                return new UserInfo
                (
                    Username: user!.UserName,
                    FirstName: user!.FirstName ?? "Unknown",
                    LastName: user!.LastName ?? "Unknown",
                    Claims: user!.Claims ?? []
                );
            }

            return null;
        }
    }
}
