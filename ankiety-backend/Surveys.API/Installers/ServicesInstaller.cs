using Surveys.Infrastructure.Services;
using Surveys.API.Installers.Interfaces;
using Surveys.Infrastructure.DataSeeder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.API.Installers
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddScoped<IDataSeeder, DataSeeder>();
            services.AddScoped<ISurveysService, SurveysService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IQuestionsService, QuestionsService>();
            services.AddScoped<IInvitationsService, InvitationsService>();
            services.AddScoped<IUsersService, UsersService>();
        }
    }
}
