using BookQuotesRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi;
using System.Net.Http.Headers;


var builder = WebApplication.CreateBuilder(args);

// Load allowed origins from configuration (appsettings.json or environment variables)
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() 
                     ?? ["http://localhost:4200"];

// Add services to the container.
builder.Services.AddControllers();
// builder.Services.AddSingleton<IRepository, LocalRepository>();
builder.Services.AddSingleton<IRepository, DbRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        // If configuration specified "*" then allow any origin (no credentials)
        if (allowedOrigins.Length == 1)
        {
            policy.WithOrigins( allowedOrigins )
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        }
        else
        {
            // Use explicit origins when credentials are allowed
            policy.WithOrigins(allowedOrigins)
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        }

        // Cache preflight responses for 10 minutes
        policy.SetPreflightMaxAge(TimeSpan.FromMinutes(10));
    });

    options.AddPolicy("AllowSpecificOriginNoCredentials", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });

    // Default permissive policy for non-production scenarios (use cautiously)
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// OpenAPI / Swagger
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    // Add JWT bearer support in Swagger UI
    options.AddSecurityDefinition( "Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer", // Or "bearer" depending on provider
        BearerFormat = "JWT", // Important for JWTs
        In = ParameterLocation.Header,
        Description = "Please insert JWT token",
        Name = "Authorization"
    } );

    options.AddSecurityRequirement( document => new( ) { [ new OpenApiSecuritySchemeReference( "Bearer", document ) ] = [ ] } );
} );

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                Convert.FromBase64String(
                    builder.Configuration["Authentication:SecretForKey"]
                    ?? throw new InvalidOperationException("JWT Key not configured.")
                )
            )
        };

        // Custom logic to read token from "CustomAuthorization" header if "Authorization" is missing
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = ctx =>
            {
                ctx.Request.Headers.TryGetValue( "Authorization", out var BearerToken );
                if (BearerToken.Count == 0)
                {
                    ctx.Request.Headers.TryGetValue( "CustomAuthorization", out var CustomBearerToken );
                    if (CustomBearerToken.Count > 0)
                    {
                        ctx.Request.Headers.Append( "Authorization", CustomBearerToken[ 0 ] );
                    }
                }
                return Task.CompletedTask;
            },
        };
    } );

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Book Quotes API V1");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

// Important: apply CORS before authentication/authorization middleware
app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
