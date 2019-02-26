using Microsoft.Azure.WebJobs.Host;
using System;
using Microsoft.Extensions.DependencyInjection;
namespace Chama.CourseManagement.Webjob
{
    public class WebJobActivator : IJobActivator
    {
        private readonly IServiceProvider services;

        public WebJobActivator(IServiceProvider services)
        {
            this.services = services;
        }

        public T CreateInstance<T>()
        {
            return services.GetService<T>();
        }
    }
}
