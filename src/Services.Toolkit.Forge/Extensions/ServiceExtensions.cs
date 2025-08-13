using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Services.Toolkit.Forge.Logging.Contracts;
using Services.Toolkit.Forge.Logging.Implementations;
using Services.Toolkit.Forge.Middlewares;

namespace Services.Toolkit.Forge.Extensions
{
    public static class ServiceExtensions
    {
        public static void UseUnifiedErrorHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<UnifiedErrorHandlerMIddleware>();
        }

        public static void AddLoggerManager(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}