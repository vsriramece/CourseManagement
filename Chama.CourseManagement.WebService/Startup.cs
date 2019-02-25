using Chama.Common.Messaging;
using Chama.CourseManagement.Infrastructure.Repository;
using Chama.CourseManagement.Infrastructure.Services;
using Chama.CourseManagement.Infrastructure.Services.Command;
using Chama.CourseManagement.Infrastructure.Services.Query;
using Chama.CourseManagement.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Chama.CourseManagement.WebService
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "CourseManagement" , Version = "v1" });
            });

            var connection = Configuration.GetConnectionString("CourseManagementDBConnection");
            var readOnlyconnection = Configuration.GetConnectionString("CourseManagementDBConnection");
            services.AddDbContext<CourseManagementDbContext>
                (options => options.UseSqlServer(connection));
            services.AddDbContext<CourseManagementReadOnlyDbContext>
               (options => options.UseSqlServer(readOnlyconnection));
            string publisherserviceBusconnection = Configuration["AppSettings:PublisherServiceBusconnection"];
            string publisherQueueName = Configuration["AppSettings:PublisherQueueName"];
            var queueClient = new QueueClient(publisherserviceBusconnection, publisherQueueName, retryPolicy:RetryPolicy.Default);
            services.AddSingleton<IMessagingClient>(new AzureServiceBusQueueClient(queueClient));
            services.AddTransient<ICoursesCommandService, CoursesCommandService>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IReportsQueryService, ReportsQueryService>();
            services.AddTransient<IReportRespository, ReportRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Course Management");
            });
        }
    }
}
