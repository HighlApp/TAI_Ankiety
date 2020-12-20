using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Surveys.API.Installers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Surveys.API.Installers
{
    public class JwtTokenInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, 
            IConfiguration configuration)
        {
            TokenValidationParameters tokenValidationParameters = 
                new TokenValidationParameters 
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtToken:Secret"])),
                    ValidIssuer = configuration["JwtToken:Issuer"],
                    ValidAudience = configuration["JwtToken:Issuer"],
                    ClockSkew = TimeSpan.Zero
                };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = bool.Parse(configuration["JwtToken:RequireHttpsMetadata"]);
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });
        }
    }
}
