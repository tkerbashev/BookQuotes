using BookQuotesRepository;
using Microsoft.OpenApi;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddSingleton<IRepository, LocalRepository>( );
builder.Services.AddSingleton<IRepository, DbRepository>( );

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc( "v1", new OpenApiInfo { Title = "My Book Quotes API", Version = "v1" } );
    // Optional: Include XML comments for better documentation
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // c.IncludeXmlComments(xmlPath);
} );

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer( options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration[ "Authentication:Issuer" ],
            ValidAudience = builder.Configuration[ "Authentication:Audience" ],
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey
            ( 
                Convert.FromBase64String
                ( 
                    builder.Configuration[ "Authentication:SecretForKey" ] ?? throw new InvalidOperationException( "JWT Key not configured." )
                ) 
            )
        };
    } );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment( ))
{
    app.MapOpenApi();
    app.UseSwagger( );
    app.UseSwaggerUI( c =>
    {
        c.SwaggerEndpoint( "/swagger/v1/swagger.json", "My Book Quotes API V1" );
        // Optional: Set a custom route prefix if needed
        // c.RoutePrefix = string.Empty; // Access at root URL
    } );
}

app.UseHttpsRedirection();

app.UseAuthentication( );

app.UseAuthorization();

app.MapControllers();

app.Run();
