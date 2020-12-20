using Surveys.Core.Settings;
using Surveys.API.Installers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Surveys.API.Installers
{
    public class SettingsInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, 
            IConfiguration configuration)
        {
            services.Configure<JwtTokenSettings>(configuration.
                GetSection("JwtToken"));
        }
    }
}
