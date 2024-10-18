using System.Runtime.CompilerServices;
using Application;
using Infrastructure.DataAccess;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Presentation.Endpoints.Abstractions;
namespace Presentation.ServicesCofiguration
{
    public static class ConfigurationExtensions
    {
        public static void RegisterAppServices(this IServiceCollection services)
        {
            //Mediatr
            services.AddMediatR(Config =>
            {
                Config.RegisterServicesFromAssembly(typeof(Application.IAssemblyMarker).Assembly);
            });

            //DataContext
            services.AddScoped<IMongoContext, MongoContext>();

            //Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public static void RegisterCustomServices(this IServiceCollection services)
        {
            
        }

        public static IServiceCollection AddEndpoints(this IServiceCollection services)
        {
            var assembly = typeof(Presentation.IAssemblyMarker).Assembly;

            ServiceDescriptor[] serviceDescriptors = assembly
                .DefinedTypes
                .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                               type.IsAssignableTo(typeof(IEndpoint)))
                .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
                .ToArray();

            services.TryAddEnumerable(serviceDescriptors);

            return services;
        }

        public static IApplicationBuilder MapEndpoints(this WebApplication app)
        {
            IEnumerable<IEndpoint> endpoints = app.Services
                .GetRequiredService<IEnumerable<IEndpoint>>();

            foreach (IEndpoint endpoint in endpoints)
            {
                endpoint.MapEndpoint(app);
            }

            return app;
        }
    }
}
