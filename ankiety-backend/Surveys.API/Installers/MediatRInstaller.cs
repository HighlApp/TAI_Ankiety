using Surveys.API.Installers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Surveys.API.Installers
{
    public class MediatRInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, 
            IConfiguration configuration)
        {
        }
    }
}
