using System.Runtime.CompilerServices;
using System.Text;
using Application;
using Application.InternalServices;
using Application.InternalServices.Abstractions;
using Domain.ServiceProviderAggregate;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Presentation.Endpoints.Abstractions;
namespace Presentation.ServicesCofiguration
{
    public static class ConfigurationExtensions
    {
        public static void RegisterAppServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            //Mediatr
            services.AddMediatR(Config =>
            {
                Config.RegisterServicesFromAssembly(typeof(Application.IAssemblyMarker).Assembly);
            });

            //DataContext
            services.AddScoped<IMongoContext, MongoContext>();

            // Authentication &  Jwt Bearer
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["AuthOptions:IssuerAudience"],
                    ValidIssuer = builder.Configuration["AuthOptions:IssuerAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                       .GetBytes(builder.Configuration["AuthOptions:Key"]))
                };
            });
            services.AddAuthorization();
            //Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "ReservationService API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                     {
                        new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference
                             {
                                 Type=ReferenceType.SecurityScheme,
                                 Id="Bearer"
                             }
                        },
                        Array.Empty<string>()
                     }
                });
            });

            //ExceptionHandler
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

        }

        public static void RegisterCustomServices(this IServiceCollection services)
        {
            //TokenGenerator
            services.AddSingleton<ITokenGenerator, TokenGenerator>();
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
