using Chama.CourseManagement.Infrastructure.Repository;
using Chama.CourseManagement.Infrastructure.UnitOfWork;
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
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
           

            var builder = new HostBuilder();
            builder.ConfigureWebJobs(b =>
            {
                b.AddAzureStorageCoreServices();
                b.AddServiceBus();
                
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
         

        }
    }

    public class JobActivator : IJobActivator
    {
        private readonly IServiceProvider services;

        public JobActivator(IServiceProvider services)
        {
            this.services = services;
        }

        public T CreateInstance<T>()
        {
            return services.GetService<T>();
        }
    }
}
