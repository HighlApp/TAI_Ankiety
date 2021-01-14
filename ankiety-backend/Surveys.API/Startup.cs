using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Surveys.API.Installers.Extension;
using Surveys.Infrastructure.DataSeeder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Surveys.Infrastructure.Services.Interfaces;
using Surveys.Infrastructure.Services;
using Surveys.Infrastructure.Settings;

namespace Surveys.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallServicesInAssembly(Configuration);
            //Add CORS
            services.AddCors();
            //Inject appsettings
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            //Add EmailSender
            services.AddTransient<IEmailSender, EmailSender>();
            //Inject EmailServiceOptions
            services.Configure<EmailServiceOptions>(options =>
            {
                options.ApiKey = Configuration["ExternalProviders:SendGrid:ApiKey"];
                options.SenderEmail = Configuration["ExternalProviders:SendGrid:SenderEmail"];
                options.SenderName = Configuration["ExternalProviders:SendGrid:SenderName"];
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IDataSeeder seeder)
        {
            if (env.IsDevelopment())
            {
                seeder.SeedDatabase();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Surveys.API v1"));
            }
            app.UseCors(builder =>
                builder.WithOrigins(Configuration["ApplicationSettings:ClientUrl"])
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
