using Microsoft.EntityFrameworkCore;
using Surveys.Infrastructure.Contexts;
using Surveys.API.Installers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Surveys.API.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<SurveysContext>(options =>
                options.UseSqlServer(configuration["ConnectionString:SurveyDB"]));
        }
    }
}
