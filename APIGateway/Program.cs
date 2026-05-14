using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

namespace Gateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", policy =>
                {
                    policy.WithOrigins("https://polite-dune-06f0adf10.7.azurestaticapps.net")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };
            });

            builder.Services.AddAuthorization();
            builder.Services.AddOcelot(builder.Configuration);

            var app = builder.Build();

            app.UseCors("AllowAngular");

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            await app.UseOcelot();

            app.Run();
        }
    }
}