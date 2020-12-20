using System;
using System.Linq;
using System.Collections.Generic;
using Surveys.API.Installers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Surveys.API.Installers.Extension
{
    public static class InstallerExtension
    {
        public static void InstallServicesInAssembly(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            List<IInstaller> installers = 
                typeof(Startup).Assembly.ExportedTypes.Where(x =>
                    typeof(IInstaller).IsAssignableFrom(x) && 
                        !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            installers.ForEach(installer => 
                installer.InstallServices(services, configuration));
        }
    }
}
