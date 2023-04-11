using Serilog;

namespace BackEndTestTask.AppSettings
{
    public static class SeriologRegistration
    {
        public static IServiceCollection ConfigureSeriologRegistration(
            this IServiceCollection services, IConfiguration _configuration)
        {
            // Register logging using Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(_configuration)
                .CreateLogger();
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog();
            });
            return services;
        }
    }
}
