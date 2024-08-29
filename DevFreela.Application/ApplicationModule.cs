using DevFreela.Application.Commands.ProjectCommands.InsertProject;
using DevFreela.Application.Models;
using DevFreela.Application.Notifications.ProjectCreated;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddHandlers();

            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssemblyContaining<InsertProjectCommand>();
                cfg.RegisterServicesFromAssemblyContaining<ProjectCreatedNotification>();
            });

            services.AddTransient<IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>, ValidateInsertProjectCommandBehavior>();

            return services;
        }
    }
}
