using EnterpriseDashboard.Api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register PostgreSQL DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql((builder.Configuration.GetConnectionString("DefaultConnection")))
);

// Configure CORS for SvelteKit local development
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowSvelteKit",
        policty => policty.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod()
    );
});

// Configure JWT Bearer Auth
var supabaseUrl = "https://clpbfnfdvqfjfnbwythg.supabase.co";

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"{supabaseUrl}/auth/v1";
        options.MetadataAddress = $"{supabaseUrl}/auth/v1/.well-known/openid-configuration";
        options.TokenValidationParameters =
            new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = $"{supabaseUrl}/auth/v1",
                ValidateAudience = true,
                ValidAudience = "authenticated",
                ValidateIssuerSigningKey = true, // Default Supabase JWT audience
            };
    });

builder.Services.AddControllers();
var app = builder.Build();

app.UseCors("AllowSvelteKit");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
