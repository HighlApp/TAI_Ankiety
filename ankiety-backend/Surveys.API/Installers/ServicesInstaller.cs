using Surveys.Infrastructure.Services;
using Surveys.API.Installers.Interfaces;
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

            services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
