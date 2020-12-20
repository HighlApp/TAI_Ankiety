using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Surveys.API.Installers.Interfaces
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
