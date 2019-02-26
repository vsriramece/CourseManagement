using Chama.Common.Email;
using Chama.CourseManagement.Infrastructure.Repository;
using Chama.CourseManagement.Infrastructure.Services;
using Chama.CourseManagement.Infrastructure.Services.Command;
using Chama.CourseManagement.Infrastructure.UnitOfWork;
using Chama.CourseManagement.Webjob.MessageProcessor;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace Chama.CourseManagement.Webjob
{
    class Program
    {
        internal static IConfiguration Configuration { get; set; }
        internal static IServiceCollection Services { get; set; }
        static void Main(string[] args)
        {
            //IServiceCollection services = new ServiceCollection();
            //ConfigureServices(services);
           

            var builder = new HostBuilder();
            builder.ConfigureWebJobs(b =>
            {
                b.AddAzureStorageCoreServices();
                b.AddServiceBus();
                
            });
            builder.ConfigureServices((hostBuilderContext, services) =>
            { 
                ConfigureServices(services);
                services.AddSingleton<IJobActivator>(new WebJobActivator(services.BuildServiceProvider()));
            });
            var host = builder.Build(); 


            using (host)
            {
                host.Run();
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            services.AddSingleton(Configuration);
            var connection = Configuration.GetConnectionString("CourseManagementDBConnection");
            services.AddDbContext<CourseManagementDbContext>
                (options => options.UseSqlServer(connection));
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICoursesCommandService, CoursesCommandService>();
            services.AddTransient<CourseSignupProcessor>();
            services.AddSingleton<IEmailProvider, EmailProvider>();
        }
    }
}
