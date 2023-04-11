using BackEndTestTask.Services.Business;
using BackEndTestTask.Services.Interfaces;
using BackEndTestTask.Models.Repositories;
using BackEndTestTask.Models.Repositories.Interfaces;

namespace BackEndTestTask.AppSettings
{
    public static class ServicesRegistration
    {
        public static IServiceCollection RegisterDependncyInjection(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<INodeService, NodeService>();
            services.AddScoped<IExceptionJournalService, ExceptionJournalService>();
            return services;
        }
    }
}
