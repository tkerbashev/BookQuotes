using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookQuotes.Controllers
{
    [ApiController]
    [Route( "[controller]" )]
    public class AuthenticationController( IConfiguration configuration ) : ControllerBase
    {
        public record AuthenticationRequestBody( string? Username, string? Password );

        private record UserInfo( string UserId, string Username, string FirstName, string LastName );

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

            var claimsForToken = new List<Claim>
            {
                new( "sub", user.UserId ),
                new( "given_name", user.FirstName ),
                new( "family_name", user.LastName ),
                new( "list_authors", "true" )
            };

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

        private static UserInfo? ValidateUserCredentials( string? username, string? password )
        {
            // Check the provided username and password against your user store (e.g., database)
            if ( username == "user" && password == "password" )
            {
                return new UserInfo
                (
                    UserId: "1",
                    Username: "testuser",
                    FirstName: "Test",
                    LastName: "User"
                );
            }

            return null;
        }
    }
}
