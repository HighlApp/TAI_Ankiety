using Surveys.API.Installers.Interfaces;
using Microsoft.Extensions.Configuration;
using Surveys.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Surveys.Infrastructure.Repositories.Interfaces;

namespace Surveys.API.Installers
{
    public class RepositoriesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISurveysRepository, SurveysRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IQuestionsRepository, QuestionsRepository>();
            services.AddScoped<IInvitationsRepository, InvitationsRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        }
    }
}
